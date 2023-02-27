using data.model;

namespace logic.Service.Inreface;

public interface IUserServer
{
    Task<PageEntity<UserEntity>> GetUsers( int? page, int? size); // получение всех объектов
 
    Task<UserEntity> GetUser(Guid id); // получение одного объекта по id
    Task<UserEntity> EditUser(UserEntity userEntity);
    Task CreateAdmin(UserEntity userEntity);
    Task<UserEntity> ChangeBlockUser(Guid id, bool value);
    Task<List<ShopEntity>> GetFavoriteShops(Guid userid);
    Task<ShopEntity> CreateFavShop(Guid shopid, Guid userid);
    Task<ShopEntity> DelFavShop(Guid shopid, Guid userid);
}