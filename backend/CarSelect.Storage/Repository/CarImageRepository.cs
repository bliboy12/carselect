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
            partitionKey: new PartitionKey(carImageModel.Id)
        );
    }

    public async Task DeleteCarImageAsync(string carImageId)
    {
        await _container.DeleteItemAsync<CarImageDataModel>(
            id: carImageId,
            partitionKey: new PartitionKey(carImageId)
        );
    }

    public async Task<IEnumerable<CarImageDataModel>> GetAllCarImagesByListingIdAsync(string listingId)
    {
        var sqlDef = new QueryDefinition("SELECT * FROM c WHERE c.listingId=@listingId").WithParameter("@listingId", listingId);
        var query = _container.GetItemQueryIterator<CarImageDataModel>(sqlDef);

        List<CarImageDataModel> carImages = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            carImages.AddRange(response.Resource);
        }
        return carImages;
    }

    public async Task<CarImageDataModel> GetCarImageByIdAsync(string carImageId)
    {
        var result = await _container.ReadItemAsync<CarImageDataModel>(
            id: carImageId,
            partitionKey: new PartitionKey(carImageId)
        );

        return result;
    }
    public async Task<IEnumerable<CarImageDataModel>> GetAllFirstImagesOfAllListings()
    {
        var sqlDef = new QueryDefinition("SELECT DISTINCT * FROM c");
        var query = _container.GetItemQueryIterator<CarImageDataModel>(sqlDef);

        List<CarImageDataModel> carImages = new();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            carImages.AddRange(response);
        }
        return carImages;
    }
}