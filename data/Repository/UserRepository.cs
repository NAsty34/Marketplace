using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class UserRepository : BaseRepository<User>, IRepositoryUser

{
    public User? GetUser(string email)
    {
     return _dbSet.FirstOrDefault(u=>u.Email == email);
    }

    public UserRepository(DBContext _dbContext) : base(_dbContext, _dbContext.Users)
    {
    }
}