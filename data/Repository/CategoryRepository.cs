using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class CategoryRepository:BaseRepository<Category>
{
    
    public CategoryRepository(DBContext _dbContext) : base(_dbContext)
    {
    }

    public IEnumerable<Guid> Children(Guid parentid)
    {
        return _dbSet.FromSqlRaw($"with recursive cte(\"Id\", \"parentId\") as (select \"Id\", \"parentId\" from \"Categories\" where \"Id\" = '{parentid.ToString()}' union all select t.\"Id\", t.\"parentId\" from \"Categories\" t inner join cte on t.\"parentId\" = cte.\"Id\") select \"Id\" from cte").Skip(1).Select(a=>a.Id).ToList();
    }
}