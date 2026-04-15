using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/listing")]
public class ListingController
{
    [HttpGet]
    public ListingResponseContract[] GetAllListings()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public ListingResponseContract GetListingWithId([FromRoute] int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public ListingResponseContract CreateListing([FromBody] ListingRequestContract listingRequest)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public ListingResponseContract UpdateListing([FromRoute] int id, [FromBody] ListingRequestContract updateListing)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("{id}")]
    public void RemoveListing([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
    [HttpGet("{id}/images")]
    public ICollection<string> GetAllImages([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
    // This is still a question because once the frontend has everything it can used that instead of calling for API again.
    // Another thing, perhaps we can change this to return one image. that way we can display the first image for each listing instead of loading everything
    // Pethaps with a query /?1
    [HttpGet("{id}/images/{imageId}")]
    public string GetImageWithId([FromRoute] int id, [FromRoute] int imageId)
    {
        throw new NotImplementedException();
    }
}