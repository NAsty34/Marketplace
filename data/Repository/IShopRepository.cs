using data.model;
namespace data.Repository;

public interface IShopRepository
{
     IEnumerable<Shop> GetShops(); // получение всех объектов
            void Create(Shop shop); // создание объекта
            void Deleted(Shop shop);
            void Save();  // сохранение изменений
}