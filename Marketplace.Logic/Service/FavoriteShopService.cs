using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class FavoriteShopService:IFavoriteShopService
{
    private IShopService _shopService;
    private IFavoriteShopRepository _favoriteShopRepository;

    public FavoriteShopService(IShopService shopService, IFavoriteShopRepository favoriteShopRepository)
    {
        _shopService = shopService;
        _favoriteShopRepository = favoriteShopRepository;
    }

    public async Task<PageEntity<FavoriteShopsEntity>> GetFavoriteShops(Guid userid, int page, int size)
    {
        return await _favoriteShopRepository.GetPageFav(userid, page, size);
        
    }

    public async Task<FavoriteShopsEntity> CreateFavShop(FavoriteShopsEntity favoriteShopsEntity)
    {
        var shop = await _shopService.GetShop(favoriteShopsEntity.ShopId);
        if (shop == null || !shop.IsActive) throw new ShopNotFoundException();

        await _favoriteShopRepository.Create(favoriteShopsEntity);
        await _favoriteShopRepository.Save();
        return favoriteShopsEntity;
    }
    public async Task<string> DelFavShop(Guid shopid, Guid userid)
    {
        var shop = await _shopService.GetShop(shopid);
        if (shop == null || !shop.IsActive) throw new ShopNotFoundException();
        var favShop = await _favoriteShopRepository.GetById(shopid, userid);
        await _favoriteShopRepository.DeleteAll(favShop);
        await _favoriteShopRepository.Save();
        return new("Магазин удален из избранных");
    }
}