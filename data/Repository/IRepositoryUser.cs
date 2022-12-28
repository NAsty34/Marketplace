using data.model;

namespace data.Repository;

public interface IRepositoryUser
  
{
    IEnumerable<User> GetUsers(); // получение всех объектов
        User? GetUser(string email); // получение одного объекта по id
        User? GetUserId(int id); // получение одного объекта по id
        void Create(User item); // создание объекта
        void Deleted(User item); //удаление объекта
        void Save();  // сохранение изменений
}