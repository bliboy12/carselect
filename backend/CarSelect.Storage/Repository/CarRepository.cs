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

    // ONLY FOR ADMINS
    public async Task<IEnumerable<CarDataModel>> GetAllCarsAsync()
    {
        var query = _container.GetItemQueryIterator<CarDataModel>(new QueryDefinition("SELECT * FROM c"));

        var results = new List<CarDataModel>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);
        }
        return results;
    }

    // Add filter conditions to be easier to put it in a SQL Query
    private static void AddFilter(List<string> conditions, List<(string name, object value)> parameters, string condition, string paramName, object? value)
    {
        if (value is string s && string.IsNullOrEmpty(s)) return;
        if (value is null) return;

        conditions.Add(condition);
        parameters.Add((paramName, value));
    }

    public async Task<IEnumerable<CarDataModel>> GetAllCarsByFilterAsync(CarFilter filter)
    {
        var conditions = new List<string>();
        var parameters = new List<(string name, object value)>();

        AddFilter(conditions, parameters, "brand=@brand", "@brand", filter.Brand);
        AddFilter(conditions, parameters, "model=@model", "@model", filter.Model);
        AddFilter(conditions, parameters, "color=@color", "@color", filter.Color);
        AddFilter(conditions, parameters, "trim=@trim", "@trim", filter.Trim);
        AddFilter(conditions, parameters, "buildYear=@buildYear", "@buildYear", filter.BuildYear);
        AddFilter(conditions, parameters, "fuel=@fuel", "@fuel", filter.Fuel);
        AddFilter(conditions, parameters, "transmission=@transmission", "@transmission", filter.Transmission);
        AddFilter(conditions, parameters, "kilometers <= @minKilometer", "@minKilometer", filter.MinKilometers);
        AddFilter(conditions, parameters, "kilometers >= @maxKilometer", "@maxKilometer", filter.MaxKilometers);
        AddFilter(conditions, parameters, "doors=@doors", "@doors", filter.Doors);
        AddFilter(conditions, parameters, "drive=@drive", "@drive", filter.Drive);

        var sql = "SELECT * FROM c";
        if (conditions.Count > 0)
            sql += " WHERE " + string.Join(" AND ", conditions);

        var queryDef = new QueryDefinition(sql);
        foreach (var (name, value) in parameters)
            queryDef.WithParameter(name, value);

        var query = _container.GetItemQueryIterator<CarDataModel>(queryDef);

        var results = new List<CarDataModel>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);
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
        catch (CosmosException)
        {
            return null;
        }
    }

    public async Task RemoveCarAsync(string carId)
    {
        try
        {
            await _container.DeleteItemAsync<CarDataModel>(
                id: carId,
                partitionKey: new PartitionKey(carId)
            );
        }
        catch (CosmosException)
        {
            throw new NotFoundException($"Car with id {carId} was not found");
        }
    }

    public async Task<CarDataModel> UpdateCarAsync(CarDataModel updateCar)
    {
        try
        {
            var response = await _container.ReplaceItemAsync<CarDataModel>(
                id: updateCar.Id,
                item: updateCar,
                partitionKey: new PartitionKey(updateCar.Id)
            );
            return response.Resource;
        }
        // only catch 404 Not Found errors, everything else will bubble up.
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"Car with id {updateCar.Id} was not found");
        }
    }
}