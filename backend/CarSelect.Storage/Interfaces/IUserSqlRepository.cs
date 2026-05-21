public interface IUserSqlRepository
{
    Task<UserDataModelSQL> CreateUserAsync(UserDataModelSQL user);
    Task<UserDataModelSQL> GetUserByIdAsync(Guid userId);
    Task DeleteUserByIdAsync(Guid userId);
    Task<IEnumerable<UserDataModelSQL>> GetAllUsers(bool newestFirst = true);
    Task<IEnumerable<UserDataModelSQL>> GetAllUsersByNameAsync(string? firstName, string? lastName);
    Task<IEnumerable<UserDataModelSQL>> SearchUserByEmail(string email);
    Task<bool> EmailExists(string email);
    Task<UserDataModelSQL> UpdateUserAsync(UserDataModelSQL updateUser);
    Task<UserDataModelSQL> UpdateUserRoleAsync(Guid userId, bool isAdmin);
}