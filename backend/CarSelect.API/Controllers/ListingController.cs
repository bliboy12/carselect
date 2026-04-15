using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ListingController
{
    [HttpGet]
    public ListingResponseContract[] GetAllListings()
    {
        throw new NotImplementedException();
    }

    [HttpGet("Id")]
    public ListingResponseContract GetListingWithId([FromRoute] int Id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public ListingResponseContract CreateListing([FromBody] ListingRequestContract listingRequest)
    {
        throw new NotImplementedException();
    }

    [HttpPut("Id")]
    public ListingResponseContract UpdateListing([FromRoute] int Id, [FromBody] ListingRequestContract updateListing)
    {
        throw new NotImplementedException();
    }
    [HttpDelete("Id")]
    public void RemoveListing([FromRoute] int Id)
    {
        throw new NotImplementedException();
    }
}