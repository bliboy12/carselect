using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/listing")]
public class ListingController : ControllerBase
{
    private readonly ICarImageService _carImageService;
    private readonly IListingService _listingService;
    public ListingController(ICarImageService carImageService, IListingService listingService)
    {
        _carImageService = carImageService;
        _listingService = listingService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListingResponseContract>>> GetAllListingsAsync()
    {
        var response = await _listingService.GetAllListingsAsync();
        List<ListingResponseContract> listings = new();

        foreach (ListingModel listing in response)
            listings.Add(ListingApiMapper.MapToContract(listing));

        return Ok(listings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ListingResponseContract>> GetListingByIdAsync([FromRoute] Guid id)
    {
        try
        {
            var result = await _listingService.GetListingByIdAsync(id);
            return Ok(ListingApiMapper.MapToContract(result));
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
    // The adding of images to the listing will be called by a separate HttpPost 
    [HttpPost]
    public async Task<ActionResult<ListingResponseContract>> CreateListing([FromBody] ListingRequestContract listingRequest)
    {
        var result = await _listingService.CreateListingAsync(ListingApiMapper.MapToDomein(listingRequest));
        return Ok(ListingApiMapper.MapToContract(result));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ListingResponseContract>> UpdateListing([FromRoute] Guid id, [FromBody] ListingRequestContract updateListing)
    {
        var convertModel = ListingApiMapper.MapToDomein(updateListing);
        convertModel.Id = id;
        var result = await _listingService.UpdateListingAsync(convertModel);

        return Ok(ListingApiMapper.MapToContract(result));
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteListingByIdAsync([FromRoute] Guid id)
    {
        await _listingService.DeletelistingById(id);
        return Ok();
    }
    [HttpGet("{id}/images")]
    public async Task<ActionResult<ICollection<CarImageResponseContract>>> GetAllImagesByListingId([FromRoute] Guid listingId)
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
    [HttpGet("{id}/images/{imageId}")]
    public async Task<ActionResult<CarImageResponseContract>> GetCarImageById([FromRoute] Guid id, [FromRoute] Guid imageId)
    {
        try
        {
            var result = await _carImageService.GetCarImageById(imageId);
            return Ok(CarImageApiMapper.MapToContract(result));
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
}