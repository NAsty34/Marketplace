using data.model;

namespace data.Repository.Interface;

public interface IRepositoryUser:IBaseRopository<User>
  
{
        User? GetUser(string email); // получение одного объекта по email
}