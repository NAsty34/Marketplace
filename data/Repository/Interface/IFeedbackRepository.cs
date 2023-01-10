using data.model;
using data.Repository.Interface;

namespace data.Repository;

public interface IFeedbackRepositiry:IBaseRopository<Feedback>
{
    Page<Feedback> GetFeedbackbyUser(int user); //получение по пользователю
    Page<Feedback> GetFeedbackbyShop(int shop); //получение по Магазину
   
}