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
            RegisterDate = userModel.RegisterDate,
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
            RegisterDate = userResponseContract.RegisterDate,
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
            RegisterDate = DateTime.Now,
            IsAdmin = false
        };
    }
}