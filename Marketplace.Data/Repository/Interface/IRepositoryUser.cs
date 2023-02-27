using data.model;

namespace data.Repository.Interface;

public interface IRepositoryUser:IBaseRopository<UserEntity>
  
{
        UserEntity? GetUser(string email); // получение одного объекта по email
}