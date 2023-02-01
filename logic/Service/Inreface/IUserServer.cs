using data.model;

namespace logic.Service;

public interface IUserServer
{
    Page<User> GetUsers(); // получение всех объектов
 
    User? GetUser(Guid id); // получение одного объекта по id
    User EditUser(User user);
    void CreateAdmin(User user);
    User ChangeBlockUser(Guid id, bool value);
    List<Shop> GetFavoriteShops(Guid userid);
    Shop CreateFavShop(Guid shopid, Guid userid);
    Shop DelFavShop(Guid shopid, Guid userid);
}