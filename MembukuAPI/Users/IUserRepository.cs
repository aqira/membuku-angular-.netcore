namespace MembukuAPI.Users;

public interface IUserRepository {
    IEnumerable<User> GetAll();
    User GetByUsername(string username);
    User Add(User user);
    User Update(User user);
    bool Delete(string username);
}
