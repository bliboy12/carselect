public class UserRepository : IUserRepository
{
    public Task<FavoriteDataModel> AddFavoriteAsync(FavoriteDataModel favoriteData)
    {
        throw new NotImplementedException();
    }

    public Task<UserDataModel> AddUserAsync(UserDataModel userDataModel)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesByUserIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDataModel>> GetAllUsers(bool newestFirst = true)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDataModel>> GetAllUsersByNameAsync(string firstName, string lastName)
    {
        throw new NotImplementedException();
    }

    public Task<UserDataModel?> GetUserByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsFavoritedAsync(int userId, int listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFavoriteAsync(int userId, int listingId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDataModel> UpdateUserAsync(UserDataModel updateUserDataModel)
    {
        throw new NotImplementedException();
    }
}