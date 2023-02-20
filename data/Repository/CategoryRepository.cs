using data.model;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class CategoryRepository:BaseRepository<Category>
{
    
    public CategoryRepository(DBContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Guid>> Children(Guid parentId)
    {
        return DbSet.FromSqlRaw($"with recursive cte(\"Id\", \"ParentId\") as (select \"Id\", \"ParentId\" from \"Categories\" where \"Id\" = '{parentId.ToString()}' union all select t.\"Id\", t.\"ParentId\" from \"Categories\" t inner join cte on t.\"Id\" = cte.\"ParentId\") select \"Id\" from cte").Skip(1).Select(a=>a.Id).ToList();
    }
}