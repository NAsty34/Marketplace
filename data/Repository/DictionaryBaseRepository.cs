using data.model;
using data.Repository.Interface;

namespace data.Repository;

public class DictionaryBaseRepository:BaseRepository<DictionaryBase>, IDictionaryBaseRepository
{
    public DictionaryBaseRepository(DBContext _dbContext) : base(_dbContext)
    {
    }
    
}