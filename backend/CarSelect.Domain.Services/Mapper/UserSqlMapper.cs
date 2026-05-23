public static class UserSqlMapper
{
    public static UserModel MapToDomein(UserDataModelSQL userDataModelSQL)
    {
        return new UserModel
        {
            Id = userDataModelSQL.Id,
            FirstName = userDataModelSQL.FirstName,
            LastName = userDataModelSQL.LastName,
            Email = userDataModelSQL.Email,
            Password = userDataModelSQL.Password,
            RegisterDate = userDataModelSQL.RegisterDate,
            IsAdmin = userDataModelSQL.IsAdmin,
            CreatedAt = userDataModelSQL.CreatedAt,
            UpdatedAt = userDataModelSQL.UpdatedAt
        };
    }
    public static UserDataModelSQL MapFromDomein(UserModel userModel)
    {
        return new UserDataModelSQL
        {
            Id = userModel.Id,
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