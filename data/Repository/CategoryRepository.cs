using data.model;
using data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace data.Repository;

public class CategoryRepository:BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DBContext _dbContext) : base(_dbContext, _dbContext.Categories)
    {
    }
}