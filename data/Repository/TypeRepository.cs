using data.Repository.Interface;

namespace data.Repository;

public class TypeRepository:BaseRepository<data.model.Type>, ITypeRepository
{
    public TypeRepository(DBContext _dbContext) : base(_dbContext, _dbContext.Types)
    {
    }
}