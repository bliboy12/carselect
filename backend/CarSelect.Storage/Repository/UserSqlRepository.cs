using Microsoft.EntityFrameworkCore;

public class UserSqlRepository : IUserSqlRepository
{
    private readonly CarSelectDbContext _context;

    public UserSqlRepository(CarSelectDbContext context)
    {
        _context = context;
    }

    public async Task<UserDataModelSQL> CreateUserAsync(UserDataModelSQL user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        user.RegisterDate = DateTime.UtcNow;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserDataModelSQL> GetUserByIdAsync(Guid userId)
    {
        var result = await _context.Users.FindAsync(userId);
        if (result == null)
            throw new NotFoundException($"User with id {userId} Not Found");
        return result;
    }

    public async Task<IEnumerable<UserDataModelSQL>> GetAllUsers(bool newestFirst = true)
    {
        return await _context.Users
            .OrderBy(u => newestFirst ? u.RegisterDate : u.RegisterDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<UserDataModelSQL>> GetAllUsersByNameAsync(string? firstName, string? lastName)
    {
        if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            return Enumerable.Empty<UserDataModelSQL>();

        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(firstName))
            query = query.Where(u => u.FirstName.StartsWith(firstName));
        if (!string.IsNullOrEmpty(lastName))
            query = query.Where(u => u.LastName.StartsWith(lastName));

        var results = await query.ToListAsync();

        if (!results.Any())
            throw new NotFoundException($"Results on FirstName {firstName} and LastName {lastName} Not Found");

        return results;
    }

    public async Task<IEnumerable<UserDataModelSQL>> SearchUserByEmail(string email)
    {
        var results = await _context.Users
            .Where(u => u.Email.StartsWith(email))
            .ToListAsync();

        if (!results.Any())
            throw new NotFoundException($"User with email {email} Not Found");

        return results;
    }

    public async Task<bool> EmailExists(string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email);
    }

    public async Task<UserDataModelSQL> UpdateUserAsync(UserDataModelSQL user)
    {
        var existing = await GetUserByIdAsync(user.Id);

        existing.FirstName = user.FirstName;
        existing.LastName = user.LastName;
        existing.Email = user.Email;
        existing.Password = user.Password;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<UserDataModelSQL> UpdateUserRoleAsync(Guid userId, bool isAdmin)
    {
        var user = await GetUserByIdAsync(userId);

        user.IsAdmin = isAdmin;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserByIdAsync(Guid userId)
    {
        var result = await GetUserByIdAsync(userId);
        _context.Users.Remove(result);
        await _context.SaveChangesAsync();
    }
}