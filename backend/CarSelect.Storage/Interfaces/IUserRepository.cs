public interface IUserRepository
{
    UserDataModel AddUser(UserDataModel userDataModel);
    UserDataModel UpdateUser(UserDataModel updateUserDataModel);
    ICollection<UserDataModel> GetAllUsers();
    UserDataModel GetUserWithId(int id);
    void RemoveUser(int id);
}