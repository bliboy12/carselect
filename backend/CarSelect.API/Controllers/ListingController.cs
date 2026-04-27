using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/listings")]
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
            return Ok(ListingApiMapper.MapToContract(result));
        }
        catch (NotFoundException nfe)
        {
            return NotFound(nfe.Message);
        }
    }
    // The adding of images to the listing will be called by a separate HttpPost 
    [HttpPost]
    public async Task<ActionResult<ListingResponseContract>> CreateListingAsync([FromBody] CreateListingRequestContract listingRequest)
    {
        var result = await _listingService.CreateListingAsync(ListingApiMapper.MapToDomein(listingRequest));
        return CreatedAtAction("CreateListing", ListingApiMapper.MapToContract(result));
    }

    // Question: is a user allowed to change the carId on a listing?
    [HttpPut("{listingId}")]
    public async Task<ActionResult<ListingResponseContract>> UpdateListingAsync([FromRoute] Guid listingId, [FromBody] ListingRequestContract updateListing)
    {
        var convertModel = ListingApiMapper.MapToDomein(updateListing);
        convertModel.Id = listingId;
        var result = await _listingService.UpdateListingAsync(convertModel);

        return Ok(ListingApiMapper.MapToContract(result));
    }
    [HttpDelete("{listingId}")]
    public async Task<ActionResult> DeleteListingByIdAsync([FromRoute] Guid listingId)
    {
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

    [HttpPost("{listingId}/images")]
    public async Task<ActionResult<IEnumerable<CarImageResponseContract>>> CreateCarImageAsync([FromRoute] Guid listingId, [FromForm] IEnumerable<IFormFile> files)
    {
        if (files == null || !files.Any())
            return BadRequest("No File(s) Provided");

        List<CarImageResponseContract> carImages = new();

        foreach (IFormFile file in files)
        {
            // the 'using' is to insure that the stream is closed instead of waiting for the garabage collector to eventually close it on its own, which is unpredictable
            using var stream = file.OpenReadStream();

            var result = await _carImageService.CreateCarImageAsync(listingId, stream, file.FileName, file.ContentType);
            carImages.Add(CarImageApiMapper.MapToContract(result));
        }
        return Ok(carImages);
    }
    [HttpPut("{listingId}/images/{imageId}")]
    public async Task<ActionResult<CarImageResponseContract>> UpdateCarImageToMainImage([FromRoute] Guid listingId, [FromRoute] Guid imageId)
    {
        var result = await _carImageService.SetMainImageAsync(listingId, imageId);

        return CarImageApiMapper.MapToContract(result);
    }

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