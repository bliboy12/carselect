using CarSelect.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/listings")]
public class ListingController : ControllerBase
{
    private readonly ICarImageService _carImageService;
    private readonly IListingService _listingService;
    private readonly UserManager<ApplicationUser> _userManager;
    public ListingController(ICarImageService carImageService, IListingService listingService, UserManager<ApplicationUser> userManager)
    {
        _carImageService = carImageService;
        _listingService = listingService;
        _userManager = userManager;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListingResponseContract>>> GetAllListingsAsync([FromQuery] Guid? sellerId)
    {
        var listings = sellerId.HasValue ? await _listingService.GetAllListingsBySellerIdAsync(sellerId.Value) : await _listingService.GetAllListingsAsync();

        var result = new List<ListingResponseContract>();
        foreach (var listing in listings)
        {
            var seller = await _userManager.FindByIdAsync(listing.SellerId.ToString());
            var contract = ListingApiMapper.MapToContract(listing);
            contract.Seller = seller != null ? UserApiMapper.MapToContract(seller) : new SellerResponseContract();
            result.Add(contract);
        }
        return Ok(result);
    }
    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<ListingResponseContract>>> FilterListingsByCarAsync([FromBody] CarFilterRequestContract carFilterRequest)
    {
        var filter = CarFilterApiMapper.MapToDomein(carFilterRequest);
        var results = await _listingService.FilterListingsByCarAsync(filter);

        var result = new List<ListingResponseContract>();
        foreach (var listing in results)
        {
            var seller = await _userManager.FindByIdAsync(listing.SellerId.ToString());
            var contract = ListingApiMapper.MapToContract(listing);
            contract.Seller = seller != null ? UserApiMapper.MapToContract(seller) : new SellerResponseContract();
            result.Add(contract);
        }
        return Ok(result);
    }

    [HttpGet("mainImages")]
    public async Task<ActionResult<CarImageResponseContract>> GetMainImagesOfAllListingsAsync()
    {
        var results = await _carImageService.GetMainImagesOfAllListingsAsync();
        List<CarImageResponseContract> carImages = new();

        foreach (CarImageModel carImage in results)
            carImages.Add(CarImageApiMapper.MapToContract(carImage));

        return Ok(carImages);
    }

    [HttpGet("{listingId}")]
    public async Task<ActionResult<ListingResponseContract>> GetListingByIdAsync([FromRoute] Guid listingId)
    {
        try
        {
            var result = await _listingService.GetListingByIdAsync(listingId);
            var seller = await _userManager.FindByIdAsync(result.SellerId.ToString());
            var contract = ListingApiMapper.MapToContract(result);
            contract.Seller = seller != null ? UserApiMapper.MapToContract(seller) : new SellerResponseContract();
            return Ok(contract);
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
    [Authorize("WritePolicy")]
    [EnableRateLimiting("QuoteCreationLimiter")]
    [HttpPost]
    public async Task<ActionResult<ListingResponseContract>> CreateListingAsync([FromBody] DetailedListingRequestContract listingRequest)
    {
        var result = await _listingService.CreateListingAsync(ListingApiMapper.MapToDomein(listingRequest));
        return CreatedAtAction("CreateListing", ListingApiMapper.MapToContract(result));
    }

    [Authorize("WritePolicy")]
    [HttpPut("{listingId}")]
    public async Task<ActionResult<ListingResponseContract>> UpdateListingAsync([FromRoute] Guid listingId, [FromBody] ListingRequestContract updateListing)
    {
        var convertModel = ListingApiMapper.MapToDomein(updateListing);
        convertModel.Id = listingId;
        var result = await _listingService.UpdateListingAsync(convertModel);

        return Ok(ListingApiMapper.MapToContract(result));
    }
    [Authorize("WritePolicy")]
    [HttpDelete("{listingId}")]
    public async Task<ActionResult> DeleteListingByIdAsync([FromRoute] Guid listingId)
    {
        // TODO: any user that has write policy can delete someone elses listing
        // We need to change this for guard for this in all methods that have similar characteristic
        await _listingService.DeletelistingById(listingId);
        return Ok();
    }

    // CAR IMAGES 

    [HttpGet("{listingId}/images")]
    public async Task<ActionResult<ICollection<CarImageResponseContract>>> GetAllImagesByListingIdAsync([FromRoute] Guid listingId)
    {
        var response = await _carImageService.GetAllCarImagesByListingIdAsync(listingId);
        List<CarImageResponseContract> carImages = new();

        foreach (CarImageModel carImage in response)
            carImages.Add(CarImageApiMapper.MapToContract(carImage));

        return Ok(carImages);
    }
    // This is still a question because once the frontend has everything it can used that instead of calling for API again.
    // Another thing, perhaps we can change this to return one image. that way we can display the first image for each listing instead of loading everything
    // Pethaps with a query /?1
    [HttpGet("{listingId}/images/{imageId}")]
    public async Task<ActionResult<CarImageResponseContract>> GetCarImageByIdAsync([FromRoute] Guid listingId, [FromRoute] Guid imageId)
    {
        try
        {
            var result = await _carImageService.GetCarImageByIdAsync(listingId, imageId);
            return Ok(CarImageApiMapper.MapToContract(result));
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
    [Authorize("AuthenticatedUser")]
    [HttpPost("{listingId}/images")]
    public async Task<ActionResult<IEnumerable<CarImageResponseContract>>> CreateCarImageAsync([FromRoute] Guid listingId, [FromForm] IEnumerable<CarImageRequestContract> files)
    {
        if (files == null || !files.Any())
            return BadRequest("No File(s) Provided");

        List<CarImageResponseContract> carImages = new();

        foreach (CarImageRequestContract file in files)
        {
            // the 'using' is to insure that the stream is closed instead of waiting for the garabage collector to eventually close it on its own, which is unpredictable
            using var stream = file.File.OpenReadStream();

            var result = await _carImageService.CreateCarImageAsync(listingId, stream, file.File.FileName, file.File.ContentType, file.IsMainImage);
            carImages.Add(CarImageApiMapper.MapToContract(result));
        }
        return Ok(carImages);
    }
    [Authorize("AuthenticatedUser")]
    [HttpPut("{listingId}/images/{imageId}")]
    public async Task<ActionResult<CarImageResponseContract>> UpdateCarImageToMainImage([FromRoute] Guid listingId, [FromRoute] Guid imageId)
    {
        var result = await _carImageService.SetMainImageAsync(listingId, imageId);

        return CarImageApiMapper.MapToContract(result);
    }
    [Authorize("AuthenticatedUser")]
    [HttpDelete("{listingId}/images/{imageId}")]
    public async Task<ActionResult> DeleteCarImageByIdAsync([FromRoute] Guid listingId, [FromRoute] Guid imageId)
    {
        try
        {
            await _carImageService.DeleteCarImageByIdAsync(listingId, imageId);
            return Ok();
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
    [Authorize("AuthenticatedUser")]
    [HttpDelete("{listingId}/images")]
    public async Task<ActionResult> DeleteAllImagesByListingIdAsync([FromRoute] Guid listingId)
    {
        try
        {
            await _listingService.DeletelistingById(listingId);
            return Ok();
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
}