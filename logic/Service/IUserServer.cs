using data.model;

namespace logic.Service;

public interface IUserServer
{
    IEnumerable<User> GetUsers(); // получение всех объектов
    IEnumerable<Feedback> GetFeedback(int userId); //отзывы пользоветаля
    User? GetUser(int id); // получение одного объекта по id
    
}