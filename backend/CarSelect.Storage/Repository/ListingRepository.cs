using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

public class ListingRepository : IListingRepository
{
    private readonly Container _listingContainer;
    private readonly Container _favoriteContainer;
    public ListingRepository(IOptions<ListingRepositoryOptions> listingOptions, IOptions<FavoriteRepositoryOptions> favoriteOptions, IOptions<CosmosOptions> cosmosOptions)
    {
        var client = new CosmosClient(cosmosOptions.Value.Connectionstring);
        _listingContainer = client.GetDatabase(cosmosOptions.Value.DatabaseName).GetContainer(listingOptions.Value.ContainerName);
        _favoriteContainer = client.GetDatabase(cosmosOptions.Value.DatabaseName).GetContainer(favoriteOptions.Value.ContainerName);

    }
    public Task<CarImageDataModel> AddCarImageAsync(CarImageDataModel carImageData)
    {
        throw new NotImplementedException();
    }

    public async Task<ListingDataModel> AddListing(ListingDataModel listingData)
    {
        // the reason for the partitionKey being SellerId is because this will be one of my more expensive Queries that I can retrieve quicker to do this.
        // All the other expensive queries are cross-partition and can't do much to it.
        var createdListing = await _listingContainer.CreateItemAsync<ListingDataModel>(
            item: listingData,
            partitionKey: new PartitionKey(listingData.Id)
        );
        return createdListing.Resource;
    }

    public Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(string listingId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesWithListingIdAsync(string userId, string listingId)
    {
        var sql = new QueryDefinition($"SELECT * FROM c WHERE c.userId=@userId AND c.listingId=@listingId")
        .WithParameter("@userId", userId)
        .WithParameter("@listingId", listingId);

        var query = _favoriteContainer.GetItemQueryIterator<FavoriteDataModel>(sql);
        List<FavoriteDataModel> results = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);
        }
        return results;
    }

    public async Task<IEnumerable<ListingDataModel>> GetAllListings()
    {
        var sql = new QueryDefinition("SELECT * FROM c");
        var query = _listingContainer.GetItemQueryIterator<ListingDataModel>(sql);

        var results = new List<ListingDataModel>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);

        }
        return results;
    }

    public async Task<IEnumerable<ListingDataModel>> GetAllListingsBySellerId(string sellerId)
    {

        var sql = new QueryDefinition("SELECT * FROM c WHERE c.sellerId=@sellerId").WithParameter("@sellerId", sellerId);

        // this just creates query not sending anything out to the network, that's why we don't await this.
        var query = _listingContainer.GetItemQueryIterator<ListingDataModel>(sql);

        var results = new List<ListingDataModel>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            // unpacks the items and adds them to the list
            results.AddRange(response.Resource);
        }
        return results;

    }

    public Task<CarImageDataModel?> GetFirstCarImageByListingIdAsync(string listingId)
    {
        throw new NotImplementedException();
    }

    public async Task<ListingDataModel> GetListingById(string listingId)
    {
        try
        {
            var result = await _listingContainer.ReadItemAsync<ListingDataModel>(
                id: listingId,
                partitionKey: new PartitionKey(listingId)
            );

            return result.Resource;
        }
        catch (CosmosException cs) when (cs.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"listing with Id {listingId} Not Found");
        }
    }


    public Task RemoveCarImageAsync(string carImageId)
    {
        throw new NotImplementedException();
    }

    public Task<CarImageDataModel> UpdateCarImageAsync(CarImageDataModel carImageData)
    {
        throw new NotImplementedException();
    }
}