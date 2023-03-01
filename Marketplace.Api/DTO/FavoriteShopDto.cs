using data.model;

namespace Marketplace.DTO;

public class FavoriteShopDto 
{
    public FavoriteShopDto()
    {
        
    }
    public FavoriteShopDto(FavoriteShopsEntity favoriteShopsEntity)
    {
        Shop = new ShopDto(favoriteShopsEntity.Shop);
        User = new UserDto(favoriteShopsEntity.User);
    }
    public ShopDto Shop { get; set; }
    public UserDto User { get; set; }
}