public interface IUserRepository
{
    Task<UserDataModel> CreateUserAsync(UserDataModel userDataModel);
    Task<UserDataModel> GetUserByIdAsync(string userId);
    Task<IEnumerable<UserDataModel>> GetAllUsersByNameAsync(string? firstName, string? lastName);
    //Task<IEnumerable<UserDataModel>> OrderByNewestUsersAsync(); ==>
    //   ==> Both bad design. Because every sorting will need a independent method which isn't very practical.
    //Task<IEnumerable<UserDataModel>> OrderByOldestUsersAsync(); ==>
    Task<UserDataModel> UpdateUserAsync(UserDataModel updateUserDataModel);
    Task<IEnumerable<UserDataModel>> GetAllUsers(bool newestFirst = true); // solution, put it as a parameter with boolean either Sort True=Newest, False=Oldest
    Task<IEnumerable<UserDataModel>> SearchUserByEmail(string email);
    Task<bool> EmailExists(string email);
    //Task DeleteUserAsync(string userId);

    // Favorites
    Task<FavoriteDataModel> AddFavoriteAsync(FavoriteDataModel favoriteData);
    Task<IEnumerable<FavoriteDataModel>> GetAllFavoritesByUserIdAsync(string userId);
    Task<bool> IsFavoritedAsync(string userId, string listingId); // Does a certain favorite exist
    Task DeleteFavoriteAsync(string userId, string listingId);
}