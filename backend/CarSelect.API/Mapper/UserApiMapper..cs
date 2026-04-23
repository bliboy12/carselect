public class UserApiMapper
{
    public static UserResponseContract MapToResponse(UserModel userModel)
    {
        return new UserResponseContract
        {
            Id = userModel.Id,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email,
            CreatedAt = userModel.CreatedAt,
            UpdatedAt = userModel.UpdatedAt,
            IsAdmin = userModel.IsAdmin
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
}