using CarSelect.Identity.Models;

public class UserApiMapper
{
    public static UserResponseContract MapToResponse(ApplicationUser user)
    {
        return new UserResponseContract
        {
            Id = Guid.Parse(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? string.Empty,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdateAt,
            IsAdmin = false
        };
    }

    public static UserModel MapToDomein(UserResponseContract userResponseContract)
    {
        return new UserModel
        {
            Id = userResponseContract.Id,
            FirstName = userResponseContract.FirstName,
            LastName = userResponseContract.LastName,
            Email = userResponseContract.Email,
            CreatedAt = userResponseContract.CreatedAt,
            UpdatedAt = userResponseContract.UpdatedAt,
            IsAdmin = userResponseContract.IsAdmin
        };
    }

    public static UserModel MapToDomein(UserRequestContract userRequestContract)
    {
        return new UserModel
        {
            FirstName = userRequestContract.FirstName,
            LastName = userRequestContract.LastName,
            Email = userRequestContract.Email,
            IsAdmin = false
        };
    }

    public static SellerResponseContract MapToContract(ApplicationUser user)
    {
        return new SellerResponseContract
        {
            Id = Guid.Parse(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
    public static SellerResponseContract MapToContract(UserModel userModel)
    {
        return new SellerResponseContract
        {
            Id = userModel.Id,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName
        };
    }
}