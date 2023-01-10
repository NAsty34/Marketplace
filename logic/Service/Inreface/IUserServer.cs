using data.model;

namespace logic.Service;

public interface IUserServer
{
    Page<User> GetUsers(); // получение всех объектов
    Page<Feedback> GetFeedback(int userId); //отзывы пользоветаля
    User? GetUser(int id); // получение одного объекта по id
    
}