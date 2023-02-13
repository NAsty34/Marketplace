using data.model;

namespace logic.Service;

public interface IUserServer
{
    Task<Page<User>> GetUsers(); // получение всех объектов
 
    Task<User> GetUser(Guid id); // получение одного объекта по id
    Task<User> EditUser(User user);
    void CreateAdmin(User user);
    Task<User> ChangeBlockUser(Guid id, bool value);
    Task<List<Shop>> GetFavoriteShops(Guid userid);
    Task<Shop> CreateFavShop(Guid shopid, Guid userid);
    Task<Shop> DelFavShop(Guid shopid, Guid userid);
}