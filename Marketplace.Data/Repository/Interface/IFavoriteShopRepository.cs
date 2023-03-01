using data.model;
using data.Repository.Interface;

namespace data.Repository;

public interface IFavoriteShopRepository
{
    /*public Task<PageEntity<FavoriteShopsEntity>> GetPage(IQueryable<FavoriteShopsEntity> queryable, int? page,
        int? size);*/
    public Task<PageEntity<FavoriteShopsEntity>> GetPageFav(Guid userid, int page, int size);
    public Task<FavoriteShopsEntity> Create(FavoriteShopsEntity t);
    public Task DeleteAll(FavoriteShopsEntity favoriteShopsEntity);
    public Task Save();
    public Task<FavoriteShopsEntity?> GetById(Guid shopid, Guid userid);
}