using data.model;

namespace logic.Service.Inreface;

public interface IShopService
{
    public Task<PageEntity<ShopEntity>> GetShops(FiltersShopsEntity filtersShopsEntity, int? page, int? size);
    public Task<ShopEntity> GetShop(Guid id);
    public Task DeleteShop(Guid id);
    public Task<ShopEntity> CreateShop(ShopEntity shopEntity);
    public Task<ShopEntity> EditShop(ShopEntity shopEntity, Guid userid, RoleEntity roleEntity);
    public Task<ShopEntity> ChangeBlockShop(Guid id, bool value);
    
}