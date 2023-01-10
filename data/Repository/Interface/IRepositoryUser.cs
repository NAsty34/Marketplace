using data.model;
using data.Repository.Interface;

namespace data.Repository;

public interface IRepositoryUser:IBaseRopository<User>
  
{
        User? GetUser(string email); // получение одного объекта по email
}