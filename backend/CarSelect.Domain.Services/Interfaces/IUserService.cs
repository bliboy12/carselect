public interface IUserService
{
    Task<UserModel> CreateUserAsync(UserModel userDataModel);
    Task<UserModel> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<UserModel>> GetAllUsersByNameAsync(string? firstName, string? lastName);
    //Task<IEnumerable<UserDataModel>> OrderByNewestUsersAsync(); ==>
    //   ==> Both bad design. Because every sorting will need a independent method which isn't very practical.
    //Task<IEnumerable<UserDataModel>> OrderByOldestUsersAsync(); ==>
    Task<UserModel> UpdateUserAsync(UserModel updateUserDataModel);
    Task<IEnumerable<UserModel>> GetAllUsers(bool newestFirst = true); // solution, put it as a parameter with boolean either Sort True=Newest, False=Oldest
    Task RemoveUserAsync(Guid userId);

    // Favorites
    Task<FavoriteModel> AddFavoriteAsync(FavoriteModel favoriteData);
    Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserIdAsync(Guid userId);
    Task<bool> IsFavoritedAsync(Guid userId, Guid listingId); // Does a certain favorite exist
    Task RemoveFavoriteAsync(Guid userId, Guid listingId);
}