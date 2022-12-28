using data.model;
namespace data.Repository;

public class UserRepository : IRepositoryUser

{
    public IEnumerable<User> GetUsers()
    {
        return DBContext.GetContext().Users.ToList();
    }

    public User? GetUser(string email)
    {
     return DBContext.GetContext().Users.FirstOrDefault(u=>u.Email == email);
    }

    public User? GetUserId(int id)
    {
        return DBContext.GetContext().Users.FirstOrDefault(u=>u.Id == id);
    }

    public void Create(User user)
    {
        DBContext.GetContext().Users.Add(user);
    }

    public void Deleted(User user)
    {
        DBContext.GetContext().Users.Remove(user);
    }

    public void Save()
    {
    DBContext.GetContext().SaveChanges();
    }
    
}