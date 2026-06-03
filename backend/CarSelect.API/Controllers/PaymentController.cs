using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

[ApiController]
[Authorize]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly StripeSettings _stripeSettings;
    private readonly IListingService _listingService;
    private readonly ITransactionSqlService _transactionService;
    public PaymentController(IOptions<StripeSettings> stripeSettings, IListingService listingService, ITransactionSqlService transactionSqlService)
    {
        _stripeSettings = stripeSettings.Value;
        _listingService = listingService;
        _transactionService = transactionSqlService;
    }

    [Authorize("WritePolicy")]
    [HttpPost("create-payment-intent")]
    public async Task<ActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
    {
        try
        {
            var listing = await _listingService.GetListingByIdAsync(request.ListingId);

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(listing.Price * 100), // Stripe uses cents
                Currency = "eur",
                Metadata = new Dictionary<string, string>
                {
                    { "listingId", listing.Id.ToString() },
                    { "buyerId", request.BuyerId.ToString() }
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return Ok(new
            {
                clientSecret = paymentIntent.ClientSecret,
                publishableKey = _stripeSettings.PublishableKey,
                amount = listing.Price,
                currency = "eur"
            });
        }
        catch (StripeException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("webhook")]
    public async Task<ActionResult> Webhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        Console.WriteLine($"Webhook received, payload length: {json.Length}");

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _stripeSettings.WebhookSecret
            );

            Console.WriteLine($"Event type: {stripeEvent.Type}");

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                Console.WriteLine("PaymentIntentSucceeded event detected");
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                Console.WriteLine($"PaymentIntent null: {paymentIntent == null}");

                if (paymentIntent != null)
                {
                    Console.WriteLine($"Metadata count: {paymentIntent.Metadata.Count}");
                    foreach (var key in paymentIntent.Metadata.Keys)
                        Console.WriteLine($"Metadata: {key} = {paymentIntent.Metadata[key]}");

                    if (paymentIntent.Metadata.ContainsKey("listingId") &&
                        paymentIntent.Metadata.ContainsKey("buyerId"))
                    {
                        var listingId = Guid.Parse(paymentIntent.Metadata["listingId"]);
                        var buyerId = Guid.Parse(paymentIntent.Metadata["buyerId"]);

                        Console.WriteLine($"Saving transaction for listing {listingId} by buyer {buyerId}");

                        await _transactionService.AddTransactionAsync(new TransactionModel
                        {
                            BuyerId = buyerId,
                            ListingId = listingId,
                            AgreedPrice = paymentIntent.Amount / 100m,
                            Status = "Completed"
                        });

                        Console.WriteLine("Transaction saved successfully");

                        // mark listing as sold
                        var listing = await _listingService.GetListingByIdAsync(listingId);
                        listing.Status = ListingStatus.Sold;
                        await _listingService.UpdateListingAsync(listing);

                        Console.WriteLine("Transaction saved and listing marked as sold");
                    }
                    else
                    {
                        Console.WriteLine("Missing metadata keys — transaction not saved");
                    }
                }
            }

            return Ok();
        }
        catch (StripeException ex)
        {
            Console.WriteLine($"Stripe exception: {ex.Message}");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General exception: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }
}