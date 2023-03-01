using data.model;

namespace logic.Service;

public interface IFavoriteShopService
{
    Task<PageEntity<FavoriteShopsEntity>> GetFavoriteShops(Guid userid, int page, int size);
    Task<FavoriteShopsEntity> CreateFavShop(FavoriteShopsEntity favoriteShopsEntity);
    Task<string> DelFavShop(Guid shopid, Guid userid);
}