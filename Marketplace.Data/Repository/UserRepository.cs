using data.model;
using data.Repository.Interface;

namespace data.Repository;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository

{
    public UserEntity? GetUser(string email)
    {
     return DbSet.FirstOrDefault(u=>u.Email == email);
    }

    public UserRepository(MarketplaceContext marketplaceContext) : base(marketplaceContext)
    {
    }

    
}