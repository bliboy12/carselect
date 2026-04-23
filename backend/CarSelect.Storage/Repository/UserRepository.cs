using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

public class UserRepository : IUserRepository
{
    private readonly Container _container;
    public UserRepository(IOptions<UserRepositoryOptions> userOptions, IOptions<CosmosOptions> cosmosOptions)
    {
        var client = new CosmosClient(cosmosOptions.Value.Connectionstring);
        _container = client.GetDatabase(cosmosOptions.Value.DatabaseName).GetContainer(userOptions.Value.ContainerName);
    }
    public async Task<FavoriteDataModel> AddFavoriteAsync(FavoriteDataModel favoriteData)
    {
        var createdFavorite = await _container.CreateItemAsync<FavoriteDataModel>(
            item: favoriteData,
            partitionKey: new PartitionKey($"{favoriteData.Id}")
        );

        return createdFavorite.Resource;
    }

    public async Task<UserDataModel> CreateUserAsync(UserDataModel userDataModel)
    {
        userDataModel.Id = Guid.NewGuid().ToString();
        userDataModel.CreatedAt = DateTime.Now;
        userDataModel.UpdatedAt = DateTime.Now;

        var createdFavorite = await _container.CreateItemAsync<UserDataModel>(
            item: userDataModel,
            partitionKey: new PartitionKey($"{userDataModel.Id}")
        );

        return createdFavorite.Resource;
    }

    public async Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesByUserIdAsync(string userId)
    {
        var query = _container.GetItemQueryIterator<FavoriteDataModel>(new QueryDefinition("SELECT * FROM c WHERE userId=@userId").WithParameter("@userId", userId));

        var results = new List<FavoriteDataModel>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);
        }
        return results;
    }

    public async Task<IEnumerable<UserDataModel>> GetAllUsers(bool newestFirst = true)
    {
        var query = _container.GetItemQueryIterator<UserDataModel>(new QueryDefinition("SELECT * FROM c"));

        var results = new List<UserDataModel>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.Resource);
        }
        return results;
    }

    public async Task<IEnumerable<UserDataModel>> GetAllUsersByNameAsync(string? firstName, string? lastName)
    {
        try
        {
            List<string> conditions = new();

            // here we put the conditions: names that start with the values given from the user, case-insensitive 
            if (!string.IsNullOrEmpty(firstName))
                conditions.Add("STARTSWITH(c.firstName, @firstName, true)");
            if (!string.IsNullOrEmpty(lastName))
                conditions.Add("STARTSWITH(c.lastName, @lastName, true)");

            // Returning a empty Enumerable if both parameters are empty
            if (conditions.Count == 0)
                return Enumerable.Empty<UserDataModel>();

            var sql = "SELECT * FROM c WHERE " + string.Join(" AND ", conditions);

            var queryDef = new QueryDefinition(sql);

            if (!string.IsNullOrEmpty(firstName))
                queryDef = queryDef.WithParameter("@firstName", firstName);
            if (!string.IsNullOrEmpty(lastName))
                queryDef = queryDef.WithParameter("@lastName", lastName);

            var query = _container.GetItemQueryIterator<UserDataModel>(queryDef);
            List<UserDataModel> results = new();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.Resource);
            }
            return results;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Returning nothing if both 
            throw new NotFoundException($"Results on FirstName {firstName} and LastName {lastName} Not Found");
        }
    }

    public async Task<UserDataModel> GetUserByIdAsync(string userId)
    {
        try
        {
            var result = await _container.ReadItemAsync<UserDataModel>(
                id: userId,
                partitionKey: new PartitionKey(userId)
            );

            return result.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"User with Id {userId} Not Found");
        }
    }

    public async Task<IEnumerable<UserDataModel>> SearchUserByEmail(string email)
    {
        try
        {

            QueryDefinition sql = new QueryDefinition("SELECT * FROM c WHERE STARTSWITH(c.email, @email, true)");
            var query = _container.GetItemQueryIterator<UserDataModel>(sql);

            List<UserDataModel> results = new();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.Resource);
            }
            return results;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"User with email {email} Not Found");
        }
    }

    public async Task<bool> EmailExists(string email)
    {
        QueryDefinition sql = new QueryDefinition("SELECT * FROM c WHERE c.email=@email").WithParameter("@email", email);

        var queryOption = new QueryRequestOptions { MaxItemCount = 1 };
        var query = _container.GetItemQueryIterator<UserDataModel>(sql, requestOptions: queryOption);

        while (query.HasMoreResults)
        {
            // the first match we have we check if its true otherwise its going to indicate to false
            var response = await query.ReadNextAsync();
            if (response.Resource.Any())
                return true;
        }
        return false;
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

    public async Task DeleteFavoriteAsync(string userId, string listingId)
    {
        try
        {
            var result = await _container.DeleteItemAsync<FavoriteDataModel>(
                id: $"{userId}_{listingId}",
                partitionKey: new PartitionKey($"{userId}_{listingId}")
            );
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"favorite with id {userId}_{listingId} Not Found");
        }
    }

    // public async Task DeleteUserAsync(string userId)
    // {
    //     try
    //     {
    //         var result = await _container.DeleteItemAsync<UserDataModel>(
    //             id: userId,
    //             partitionKey: new PartitionKey(userId)
    //         );
    //     }
    //     catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
    //     {
    //         throw new NotFoundException($"user with id {userId} Not Found");
    //     }
    // }

    public async Task<UserDataModel> UpdateUserAsync(UserDataModel updateUserDataModel)
    {
        try
        {
            // retrieve when the user was created and add it in to keep that information otherwise its null
            var oldUser = await GetUserByIdAsync(updateUserDataModel.Id);

            updateUserDataModel.UpdatedAt = DateTime.Now;
            updateUserDataModel.CreatedAt = oldUser.CreatedAt;

            var result = await _container.ReplaceItemAsync<UserDataModel>(
                id: updateUserDataModel.Id,
                item: updateUserDataModel,
                partitionKey: new PartitionKey(updateUserDataModel.Id)
            );

            return result.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new NotFoundException($"user with id {updateUserDataModel.Id} Not Found");
        }
    }
}