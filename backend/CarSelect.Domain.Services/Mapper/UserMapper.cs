public class UserMapper
{
    public static UserModel MapToDomein(UserDataModel userDataModel)
    {
        return new UserModel
        {
            Id = Guid.Parse(userDataModel.Id),
            FirstName = userDataModel.FirstName,
            LastName = userDataModel.LastName,
            Email = userDataModel.Email,
            Password = userDataModel.Password,
            RegisterDate = userDataModel.RegisterDate,
            IsAdmin = userDataModel.IsAdmin,
            IsDeleted = userDataModel.IsDeleted,
            CreatedAt = userDataModel.CreatedAt,
            UpdatedAt = userDataModel.UpdatedAt
        };
    }
    public static UserDataModel MapFromDomein(UserModel userModel)
    {
        return new UserDataModel
        {
            Id = userModel.Id.ToString(),
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email,
            Password = userModel.Password,
            RegisterDate = userModel.RegisterDate,
            IsAdmin = userModel.IsAdmin,
            CreatedAt = userModel.CreatedAt,
            UpdatedAt = userModel.UpdatedAt
        };
    }
}