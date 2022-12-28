using data.model;

namespace data.Repository;

public interface IFeedbackRepositiry
{
    IEnumerable<Feedback> GetFeedbackbyUser(int user); //получение по пользователю
    IEnumerable<Feedback> GetFeedbackbyShop(Shop shop); //получение по Магазину
    IEnumerable<Feedback> GetFeedbackbyUserandShop(User user, Shop shop); //получение по Магазину и пользователю
    void Create(Feedback feedback);
    void Deleted(Feedback feedback);
    void Save();
}