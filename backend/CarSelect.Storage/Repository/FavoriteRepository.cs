using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

public class FavoriteRepository : IFavoriteRepository
{
    private readonly Container _container;
    public FavoriteRepository(IOptions<FavoriteRepositoryOptions> favoriteOption, IOptions<CosmosOptions> cosmosOptions)
    {
        var client = new CosmosClient(cosmosOptions.Value.Connectionstring);
        _container = client.GetDatabase(cosmosOptions.Value.DatabaseName).GetContainer(favoriteOption.Value.ContainerName);
    }
    public async Task<FavoriteDataModel> CreateFavoriteAsync(FavoriteDataModel favoriteData)
    {

        var createdFavorite = await _container.CreateItemAsync<FavoriteDataModel>(
            item: favoriteData,
            partitionKey: new PartitionKey(favoriteData.UserId)
        );

        return createdFavorite.Resource;
    }

    public async Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesByUserIdAsync(string userId)
    {
        var query = _container.GetItemQueryIterator<FavoriteDataModel>(new QueryDefinition("SELECT * FROM c WHERE c.userId=@userId").WithParameter("@userId", userId));

        var results = new List<FavoriteDataModel>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);
        }
        return results;
    }

    public async Task<bool> IsFavoritedAsync(string userId, string listingId)
    {
        try
        {
            var result = await _container.ReadItemAsync<FavoriteDataModel>(
                id: $"{userId}_{listingId}",
                partitionKey: new PartitionKey($"{userId}_{listingId}")
            );

            return true;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
    }

    public async Task DeleteFavoriteAsyncByListingIdAsync(string userId, string listingId)
    {
        try
        {
            var result = await _container.DeleteItemAsync<FavoriteDataModel>(
                id: $"{userId}_{listingId}",
                partitionKey: new PartitionKey(userId)
            );
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"favorite with id {userId}_{listingId} Not Found");
        }
    }
}