using data.model;

namespace logic.Service;

public interface IUserServer
{
    Page<User> GetUsers(); // получение всех объектов
 
    User? GetUser(int id); // получение одного объекта по id
    User EditUser(User user);
    User? CreateAdmin(User user);
    User ChangeBlockUser(int id, bool value);
    List<Shop> GetFavoriteShops(int userid);
    Shop CreateFavShop(int shopid, int userid);
    Shop DelFavShop(int shopid, int userid);
}