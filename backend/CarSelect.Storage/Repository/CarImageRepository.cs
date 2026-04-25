using System.Data.Common;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

public class CarImageRepository : ICarImageRepository
{
    private readonly Container _container;
    public CarImageRepository(IOptions<CarImageRepositoryOptions> carImageOptions, IOptions<CosmosOptions> cosmosOptions)
    {
        var client = new CosmosClient(cosmosOptions.Value.Connectionstring);
        _container = client.GetDatabase(cosmosOptions.Value.DatabaseName).GetContainer(carImageOptions.Value.ContainerName);
    }
    public async Task<CarImageDataModel> CreateCarImageAsync(CarImageDataModel carImageModel)
    {
        return await _container.CreateItemAsync<CarImageDataModel>(
            item: carImageModel,
            partitionKey: new PartitionKey(carImageModel.ListingId)
        );
    }

    public async Task DeleteCarImageByIdAsync(string listingId, string carImageId)
    {
        await _container.DeleteItemAsync<CarImageDataModel>(
            id: carImageId,
            partitionKey: new PartitionKey(listingId)
        );
    }

    public async Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(string listingId)
    {
        QueryDefinition sqlDef = new QueryDefinition("SELECT * FROM c WHERE c.listingId=@listingId").WithParameter("@listingId", listingId);
        var query = _container.GetItemQueryIterator<CarImageDataModel>(sqlDef);

        List<CarImageDataModel> carImages = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            carImages.AddRange(response.Resource);
        }
        return carImages;
    }

    public async Task<CarImageDataModel> GetCarImageByIdAsync(string listingId, string carImageId)
    {
        var result = await _container.ReadItemAsync<CarImageDataModel>(
            id: carImageId,
            partitionKey: new PartitionKey(listingId)
        );

        return result.Resource;
    }
    // This will return all the first Images for each listingId
    public async Task<IEnumerable<CarImageDataModel>> GetMainImagesOfAllListingsAsync()
    {
        var sqlDef = new QueryDefinition("SELECT * FROM c WHERE c.isMainImage=true");
        var query = _container.GetItemQueryIterator<CarImageDataModel>(sqlDef);

        List<CarImageDataModel> carImages = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            carImages.AddRange(response.Resource);
        }
        return carImages;
    }
    public async Task<CarImageDataModel> UpdateCarImage(CarImageDataModel carImageData)
    {
        var result = await _container.ReplaceItemAsync<CarImageDataModel>(
            id: carImageData.Id,
            item: carImageData,
            partitionKey: new PartitionKey(carImageData.ListingId)
        );
        return result;
    }
    public async Task<CarImageDataModel?> GetMainImageByListingId(string listingId)
    {
        QueryDefinition sqlDef = new QueryDefinition("SELECT * FROM c WHERE c.listingId=@listingId AND c.isMainImage=true").WithParameter("@listingId", listingId);
        var query = _container.GetItemQueryIterator<CarImageDataModel>(sqlDef);

        if (query.HasMoreResults)
        {
            var result = await query.ReadNextAsync();
            return result.First();
        }
        return null;
    }
    public async Task<CarImageDataModel> SetMainImageAsync(CarImageDataModel newMainImage, CarImageDataModel? oldMainImage)
    {
        // Both CarImages share the same listingId, so we give it only once
        // Also Transactional Batch only work when both query operation that you wish to execute life on the same partition
        PartitionKey partitionKey = new PartitionKey(newMainImage.ListingId);
        TransactionalBatch batch = _container.CreateTransactionalBatch(partitionKey);

        batch.ReplaceItem<CarImageDataModel>(
            id: newMainImage.Id,
            item: newMainImage
        );
        if (oldMainImage != null)
            batch.ReplaceItem<CarImageDataModel>(id: oldMainImage.Id, item: oldMainImage);

        var response = await batch.ExecuteAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Batch failed with status: {response.StatusCode}");

        TransactionalBatchOperationResult<CarImageDataModel> carImageResponse;

        carImageResponse = response.GetOperationResultAtIndex<CarImageDataModel>(0);
        CarImageDataModel carImageDataModel = carImageResponse.Resource;

        return carImageDataModel;

    }
}