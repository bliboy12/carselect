public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    public UserService(IUserRepository repo)
    {
        _repo = repo;
    }
    public async Task<FavoriteModel> AddFavoriteAsync(FavoriteModel favoriteData)
    {
        var result = await _repo.AddFavoriteAsync(FavoriteMapper.MapFromDomein(favoriteData));
        return FavoriteMapper.MapToDomein(result);
    }

    public async Task<UserModel> AddUserAsync(UserModel userDataModel)
    {
        var result = await _repo.AddUserAsync(UserMapper.MapFromDomein(userDataModel));
        return UserMapper.MapToDomein(result);
    }

    public async Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserIdAsync(Guid userId)
    {
        IEnumerable<FavoriteDataModel> response = await _repo.GetAllFavoritesByUserIdAsync(userId.ToString());
        List<FavoriteModel> favorites = new();

        foreach (FavoriteDataModel favorite in response)
            favorites.Add(FavoriteMapper.MapToDomein(favorite));

        return favorites;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers(bool newestFirst = true)
    {
        IEnumerable<UserDataModel> response = await _repo.GetAllUsers(newestFirst);
        List<UserModel> users = new();

        foreach (UserDataModel user in response)
            users.Add(UserMapper.MapToDomein(user));

        return users;
    }

    public async Task<IEnumerable<UserModel>> GetAllUsersByNameAsync(string firstName, string lastName)
    {
        IEnumerable<UserDataModel> response = await _repo.GetAllUsersByNameAsync(firstName, lastName);
        List<UserModel> users = new();

        foreach (UserDataModel user in response)
            users.Add(UserMapper.MapToDomein(user));

        return users;
    }

    public async Task<UserModel?> GetUserByIdAsync(Guid userId)
    {
        var user = await _repo.GetUserByIdAsync(userId.ToString());

        if (user == null)
            return null;

        return UserMapper.MapToDomein(user);
    }

    public async Task<bool> IsFavoritedAsync(Guid userId, Guid listingId)
    {
        return await _repo.IsFavoritedAsync(userId.ToString(), listingId.ToString());
    }

    public async Task RemoveFavoriteAsync(Guid userId, Guid listingId)
    {
        await _repo.RemoveFavoriteAsync(userId.ToString(), listingId.ToString());
    }

    public async Task RemoveUserAsync(Guid userId)
    {
        await _repo.RemoveUserAsync(userId.ToString());
    }

    public async Task<UserModel> UpdateUserAsync(UserModel updateUserDataModel)
    {
        UserDataModel userDataModel = await _repo.UpdateUserAsync(UserMapper.MapFromDomein(updateUserDataModel));
        return UserMapper.MapToDomein(userDataModel);
    }
}