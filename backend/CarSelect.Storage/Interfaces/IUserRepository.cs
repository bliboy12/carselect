public interface IUserRepository
{
    Task<UserDataModel> AddUserAsync(UserDataModel userDataModel);
    Task<UserDataModel?> GetUserByIdAsync(int userId);
    Task<IEnumerable<UserDataModel>> GetAllUsersByNameAsync(string firstName, string lastName);
    //Task<IEnumerable<UserDataModel>> OrderByNewestUsersAsync(); ==>
    //   ==> Both bad design. Because every sorting will need a independent method which isn't very practical.
    //Task<IEnumerable<UserDataModel>> OrderByOldestUsersAsync(); ==>
    Task<UserDataModel> UpdateUserAsync(UserDataModel updateUserDataModel);
    Task<IEnumerable<UserDataModel>> GetAllUsers(bool newestFirst = true); // solution, put it as a parameter with boolean either Sort True=Newest, False=Oldest
    Task RemoveUserAsync(int id);

    // Favorites
    Task<FavoriteDataModel> AddFavoriteAsync(FavoriteDataModel favoriteData);
    Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesByUserIdAsync(int userId);
    Task<bool> IsFavoritedAsync(int userId, int listingId); // Does a certain favorite exist
    Task RemoveFavoriteAsync(int userId, int listingId);
}