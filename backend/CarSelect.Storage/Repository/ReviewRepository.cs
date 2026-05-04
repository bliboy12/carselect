using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

public class ReviewRepository : IReviewRepository
{
    private readonly Container _container;
    public ReviewRepository(IOptions<ReviewRepositoryOptions> reviewOption, IOptions<CosmosOptions> cosmosOption)
    {
        var client = new CosmosClient(cosmosOption.Value.Connectionstring);
        _container = client.GetDatabase(cosmosOption.Value.DatabaseName).GetContainer(reviewOption.Value.ContainerName);
    }
    public async Task<ReviewDataModel> CreateReviewAsync(ReviewDataModel reviewDataModel)
    {
        reviewDataModel.Id = Guid.NewGuid().ToString();

        var createdReview = await _container.CreateItemAsync(item: reviewDataModel, partitionKey: new PartitionKey(reviewDataModel.Id));
        return createdReview.Resource;
    }

    public async Task<IEnumerable<ReviewDataModel>> GetAllReviewsByReviewerIdAsync(string reviewerId)
    {
        QueryDefinition sqlDef = new QueryDefinition("SELECT * FROM c WHERE c.reviewerId=@reviewerId").WithParameter("@reviewerId", reviewerId);
        var query = _container.GetItemQueryIterator<ReviewDataModel>(sqlDef);

        List<ReviewDataModel> results = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);
        }
        return results;
    }

    public async Task<IEnumerable<ReviewDataModel>> GetAllReviewsBySellerIdAsync(string sellerId)
    {
        QueryDefinition sqlDef = new QueryDefinition("SELECT * FROM c WHERE c.sellerId=@sellerId").WithParameter("@sellerId", sellerId);
        var query = _container.GetItemQueryIterator<ReviewDataModel>(sqlDef);

        List<ReviewDataModel> results = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);
        }
        return results;
    }

    public async Task<ReviewDataModel?> GetReviewByIdAsync(string reviewId)
    {
        var result = await _container.ReadItemAsync<ReviewDataModel>(id: reviewId, partitionKey: new PartitionKey(reviewId));

        return result.Resource;
    }

    public async Task DeleteReviewByIdAsync(string reviewId)
    {
        await _container.DeleteItemAsync<ReviewDataModel>(id: reviewId, new PartitionKey(reviewId));
    }

    public async Task<ReviewDataModel> UpdateReviewByIdAsync(string reviewId, ReviewDataModel newReview)
    {
        var result = await _container.ReplaceItemAsync<ReviewDataModel>(item: newReview, reviewId, new PartitionKey(reviewId));
        return result.Resource;
    }
}