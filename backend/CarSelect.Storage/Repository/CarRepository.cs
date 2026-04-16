using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

public class CarRepository : ICarRepository
{
    private readonly Container _container;
    public CarRepository(IOptions<CarRepositoryOptions> carOptions, IOptions<CosmosOptions> cosmosOptions)
    {
        var client = new CosmosClient(cosmosOptions.Value.Connectionstring);
        _container = client.GetDatabase(cosmosOptions.Value.DatabaseName).GetContainer(carOptions.Value.ContainerName);
    }
    public async Task<CarDataModel> AddCarAsync(CarDataModel carDataModel)
    {
        var createdCar = await _container.CreateItemAsync(
            item: carDataModel,
            partitionKey: new PartitionKey(carDataModel.Id)
        );

        return createdCar.Resource;
    }

    public async Task<IEnumerable<CarDataModel>> GetAllCarsAsync()
    {
        var query = _container.GetItemQueryIterator<CarDataModel>(new QueryDefinition("SELECT * FROM cars"));

        var results = new List<CarDataModel>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task<IEnumerable<CarDataModel>> GetAllCarsByFilterAsync(CarFilter filter)
    {
        var query = _container.GetItemQueryIterator<CarDataModel>(
            new QueryDefinition("SELECT * FROM cars WHERE brand=@brand model=@model color=@color trim=@trim buildyear=@buildyear fuel=@fuel transmission=@transmission doors=@doors drive=@drive AND kilometers BETWEEN @minKilometer AND @maxKilometer")
            .WithParameter("@brand", filter.Brand)
            .WithParameter("@model", filter.Model)
            .WithParameter("@color", filter.Color)
            .WithParameter("@trim", filter.Trim)
            .WithParameter("@buildyear", filter.BuildYear)
            .WithParameter("@fuel", filter.Fuel)
            .WithParameter("@transmission", filter.Transmission)
            .WithParameter("@minKilometers", filter.MinKilometers)
            .WithParameter("@maxKilometer", filter.MaxKilometers)
            .WithParameter("@doors", filter.Doors)
            .WithParameter("@drive", filter.Drive)
        );

        var results = new List<CarDataModel>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    // public Task<IEnumerable<CarDataModel>> GetAllCarsWithOwnerIdAsync(int ownerId)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<CarDataModel?> GetCarWithIdAsync(string carId)
    {
        try
        {
            var response = await _container.ReadItemAsync<CarDataModel>(
                id: carId,
                partitionKey: new PartitionKey(carId)
            );
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public Task RemoveCarAsync(string carId)
    {
        try
        {
            var response = _container.DeleteItemAsync<CarDataModel>(
                id: carId,
                partitionKey: new PartitionKey(carId)
            );

            return response;
        }
        catch (CosmosException ex)
        {
            return null;
        }
    }

    public Task<CarDataModel> UpdateCarAsync(CarDataModel updateCar)
    {
        if (GetCarWithIdAsync(updateCar.Id) == null)
            throw new CosmosException("CarId doesn't exists");
    }
}