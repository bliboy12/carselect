public class UserSqlService : IUserSqlService
{
    private readonly IUserSqlRepository _repo;

    public UserSqlService(IUserSqlRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserModel> CreateUserAsync(UserModel userModel)
    {
        // Needs to be handled without throwing an exception!!
        if (await EmailExists(userModel.Email))
            throw new ArgumentException("Email Already Exists");

        var result = await _repo.CreateUserAsync(UserSqlMapper.MapFromDomein(userModel));
        return UserSqlMapper.MapToDomein(result);
    }


    public async Task<IEnumerable<UserModel>> GetAllUsers(bool newestFirst = true)
    {
        IEnumerable<UserDataModelSQL> response = await _repo.GetAllUsers(newestFirst);
        List<UserModel> users = new();

        foreach (UserDataModelSQL user in response)
            users.Add(UserSqlMapper.MapToDomein(user));

        return users;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersByNameAsync(string? firstName, string? lastName)
    {
        firstName = firstName?.ToLower();
        lastName = lastName?.ToLower();
        IEnumerable<UserDataModelSQL> response = await _repo.GetAllUsersByNameAsync(firstName, lastName);
        List<UserModel> users = new();

        foreach (UserDataModelSQL user in response)
            users.Add(UserSqlMapper.MapToDomein(user));

        return users;
    }

    public async Task<UserModel> GetUserByIdAsync(Guid userId)
    {
        var user = await _repo.GetUserByIdAsync(userId);

        return UserSqlMapper.MapToDomein(user);
    }

    // This is with Result Class which is a different way of handeling expected business logic "errors", like 'user already existing' = 'duplicate keys'
    // Everything needs to be refactored to this

    // public async Task<Result<IEnumerable<UserModel>>> SearchUserByEmail(string email)
    // {
    //     var result = await _repo.SearchUserByEmail(email);
    //     // This is a valid result and isn't a failure, that is why we return an empty array
    //     if (!result.Any())
    //         return Result<IEnumerable<UserModel>>.Success(Enumerable.Empty<UserModel>());

    //     List<UserModel> users = new();

    //     foreach (UserDataModelSQL user in result)
    //         users.Add(UserSqlMapper.MapToDomein(user));

    //     return Result<IEnumerable<UserModel>>.Success(users);
    // }

    public async Task<IEnumerable<UserModel>> SearchUserByEmail(string email)
    {
        var result = await _repo.SearchUserByEmail(email);
        // This is a valid result and isn't a failure, that is why we return an empty array
        if (!result.Any())
            throw new NotFoundException($"user with email: {email} Not Found");

        List<UserModel> users = new();

        foreach (UserDataModelSQL user in result)
            users.Add(UserSqlMapper.MapToDomein(user));

        return users;
    }

    public async Task<bool> EmailExists(string email)
    {
        return await _repo.EmailExists(email);
    }


    public async Task RemoveUserAsync(Guid userId)
    {
        // We can't hard delete the user so instead we soft delete and change his information to still be GPDR-compliance
        var getToDeleteUser = await _repo.GetUserByIdAsync(userId);

        getToDeleteUser.FirstName = "Deleted";
        getToDeleteUser.LastName = "User";
        getToDeleteUser.Email = $"deleted_{userId}@deleted.com";
        getToDeleteUser.IsDeleted = true;

        var deletedUser = await _repo.UpdateUserAsync(getToDeleteUser);
    }

    public async Task<UserModel> UpdateUserAsync(UserModel updateUserModel)
    {
        UserDataModelSQL UserDataModelSQL = await _repo.UpdateUserAsync(UserSqlMapper.MapFromDomein(updateUserModel));
        return UserSqlMapper.MapToDomein(UserDataModelSQL);
    }

    public async Task<UserModel> UpdateUserRoleAsync(Guid userId, bool isAdmin)
    {
        UserDataModelSQL UserDataModelSQL = await _repo.UpdateUserRoleAsync(userId, isAdmin);
        return UserSqlMapper.MapToDomein(UserDataModelSQL);
    }
}