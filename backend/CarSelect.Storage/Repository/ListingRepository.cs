using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

public class ListingRepository : IListingRepository
{
    private readonly Container _container;
    public ListingRepository(IOptions<ListingRepositoryOptions> listingOptions, IOptions<CosmosOptions> cosmosOptions)
    {
        var client = new CosmosClient(cosmosOptions.Value.Connectionstring);
        _container = client.GetDatabase(cosmosOptions.Value.DatabaseName).GetContainer(listingOptions.Value.ContainerName);
    }
    public Task<CarImageDataModel> AddCarImageAsync(CarImageDataModel carImageData)
    {
        throw new NotImplementedException();
    }

    public async Task<ListingDataModel> AddListing(ListingDataModel listingData)
    {
        // the reason for the partitionKey being SellerId is because this will be one of my more expensive Queries that I can retrieve quicker to do this.
        // All the other expensive queries are cross-partition and can't do much to it.
        var createdListing = await _container.CreateItemAsync<ListingDataModel>(
            item: listingData,
            partitionKey: new PartitionKey(listingData.SellerId)
        );
        return createdListing.Resource;
    }

    public Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesWithListingIdAsync(int userId, int listingId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ListingDataModel>> GetAllListings()
    {
        var sql = new QueryDefinition("SELECT * FROM c");
        var query = _container.GetItemQueryIterator<ListingDataModel>(sql);

        var results = new List<ListingDataModel>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);

        }
        return results;
    }

    public async Task<IEnumerable<ListingDataModel>> GetAllListingsBySellerId(int sellerId)
    {
        try
        {
            var sql = new QueryDefinition("SELECT * FROM c WHERE sellerId=@sellerId").WithParameter("@sellerId", sellerId);

            // this just creates query not sending anything out to the network, that's why we don't await this.
            var query = _container.GetItemQueryIterator<ListingDataModel>(sql);

            var results = new List<ListingDataModel>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                // unpacks the items and adds them to the list
                results.AddRange(response.Resource);
            }
            return results;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"Listing with sellerId {sellerId} Not Found");
        }
    }

    public Task<CarImageDataModel?> GetFirstCarImageByListingIdAsync(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task<ListingDataModel> GetListingById(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task<ListingDataModel> GetListingWithId(int listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveCarImageAsync(int carImageId)
    {
        throw new NotImplementedException();
    }

    public Task<CarImageDataModel> UpdateCarImageAsync(CarImageDataModel carImageData)
    {
        throw new NotImplementedException();
    }
}