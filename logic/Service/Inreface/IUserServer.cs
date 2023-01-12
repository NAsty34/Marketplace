using data.model;

namespace logic.Service;

public interface IUserServer
{
    Page<User> GetUsers(); // получение всех объектов
 
    User? GetUser(int id); // получение одного объекта по id
    User EditUser(User user);

}