using MembukuAPI.Data;

namespace MembukuAPI.Users;

public class UserRepository : IUserRepository {
    private readonly MembukuContext _context;

    public UserRepository(MembukuContext context) {
        _context = context;
    }

    public IEnumerable<User> GetAll() {
        return _context.Users.ToList();
    }

    public User GetByUsername(string username) {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public User Add(User user) {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User Update(User user) {
        _context.Users.Update(user);
        _context.SaveChanges();
        return user;
    }

    public bool Delete(string username) {
        var user = _context.Users.Find(username);
        if (user == null) return false;

        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }
}