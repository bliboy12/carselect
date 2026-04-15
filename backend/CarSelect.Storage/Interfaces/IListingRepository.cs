public interface IListingRepository
{
    ListingDataModel AddListing(ListingDataModel listingData);
    ListingDataModel GetListingWithId(int listingId);
    ICollection<ListingDataModel> GetAllListingWithSellerId(int sellerId);
    ICollection<ListingDataModel> GetAllListings();
}