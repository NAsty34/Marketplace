using data.model;
using data.Repository.Interface;

namespace data.Repository;

public interface IFeedbackRepositiry:IBaseRopository<Feedback>
{
    Page<Feedback> GetFeedbackbyUser(int user, bool active); //получение по пользователю
    Page<Feedback> GetFeedbackbyShop(int shop, bool active); //получение по Магазину
   
}