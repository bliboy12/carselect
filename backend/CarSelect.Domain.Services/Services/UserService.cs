public class UserService : IUserService
{
    private readonly IUserRepository _repo;

    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<UserModel> CreateUserAsync(UserModel userDataModel)
    {
        // Needs to be handled without throwing an exception!!
        if (await EmailExists(userDataModel.Email))
            throw new ArgumentException("Email Already Exists");

        var result = await _repo.CreateUserAsync(UserMapper.MapFromDomein(userDataModel));
        return UserMapper.MapToDomein(result);
    }


    public async Task<IEnumerable<UserModel>> GetAllUsers(bool newestFirst = true)
    {
        IEnumerable<UserDataModel> response = await _repo.GetAllUsers(newestFirst);
        List<UserModel> users = new();

        foreach (UserDataModel user in response)
            users.Add(UserMapper.MapToDomein(user));

        return users;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersByNameAsync(string? firstName, string? lastName)
    {
        firstName = firstName?.ToLower();
        lastName = lastName?.ToLower();
        IEnumerable<UserDataModel> response = await _repo.GetAllUsersByNameAsync(firstName, lastName);
        List<UserModel> users = new();

        foreach (UserDataModel user in response)
            users.Add(UserMapper.MapToDomein(user));

        return users;
    }

    public async Task<UserModel> GetUserByIdAsync(Guid userId)
    {
        var user = await _repo.GetUserByIdAsync(userId.ToString());

        return UserMapper.MapToDomein(user);
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

    //     foreach (UserDataModel user in result)
    //         users.Add(UserMapper.MapToDomein(user));

    //     return Result<IEnumerable<UserModel>>.Success(users);
    // }

    public async Task<IEnumerable<UserModel>> SearchUserByEmail(string email)
    {
        var result = await _repo.SearchUserByEmail(email);
        // This is a valid result and isn't a failure, that is why we return an empty array
        if (!result.Any())
            throw new NotFoundException($"user with email: {email} Not Found");

        List<UserModel> users = new();

        foreach (UserDataModel user in result)
            users.Add(UserMapper.MapToDomein(user));

        return users;
    }

    public async Task<bool> EmailExists(string email)
    {
        return await _repo.EmailExists(email);
    }


    public async Task RemoveUserAsync(Guid userId)
    {
        // We can't hard delete the user so instead we soft delete and change his information to still be GPDR-compliance
        var getToDeleteUser = await _repo.GetUserByIdAsync(userId.ToString());

        getToDeleteUser.FirstName = "Deleted";
        getToDeleteUser.LastName = "User";
        getToDeleteUser.Email = $"deleted_{userId}@deleted.com";
        getToDeleteUser.IsDeleted = true;

        var deletedUser = await _repo.UpdateUserAsync(getToDeleteUser);
    }

    public async Task<UserModel> UpdateUserAsync(UserModel updateUserDataModel)
    {
        UserDataModel userDataModel = await _repo.UpdateUserAsync(UserMapper.MapFromDomein(updateUserDataModel));
        return UserMapper.MapToDomein(userDataModel);
    }

    public async Task<UserModel> UpdateUserRoleAsync(Guid userId, bool isAdmin)
    {
        UserDataModel userDataModel = await _repo.UpdateUserRoleAsync(userId.ToString(), isAdmin);
        return UserMapper.MapToDomein(userDataModel);
    }
}