public interface IUserSqlService
{
    Task<UserModel> CreateUserAsync(UserModel userModel);
    Task<UserModel> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<UserModel>> GetAllUsersByNameAsync(string? firstName, string? lastName);
    //Task<IEnumerable<UserDataModel>> OrderByNewestUsersAsync(); ==>
    //   ==> Both bad design. Because every sorting will need a independent method which isn't very practical.
    //Task<IEnumerable<UserDataModel>> OrderByOldestUsersAsync(); ==>
    Task<UserModel> UpdateUserAsync(UserModel updateUserModel);
    Task<IEnumerable<UserModel>> GetAllUsers(bool newestFirst = true); // solution, put it as a parameter with boolean either Sort True=Newest, False=Oldest
    Task RemoveUserAsync(Guid userId);
    Task<IEnumerable<UserModel>> SearchUserByEmail(string email);
    Task<bool> EmailExists(string email);

    // Give Admin
    Task<UserModel> UpdateUserRoleAsync(Guid userId, bool isAdmin);
}