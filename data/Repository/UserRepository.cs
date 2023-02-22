using data.model;
using data.Repository.Interface;

namespace data.Repository;

public class UserRepository : BaseRepository<User>, IRepositoryUser

{
    public User? GetUser(string email)
    {
     return DbSet.FirstOrDefault(u=>u.Email == email);
    }

    public UserRepository(DBContext dbContext) : base(dbContext)
    {
    }

    
}