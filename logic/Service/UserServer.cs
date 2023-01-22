using data.model;
using data.Repository;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class UserServer:IUserServer
{
    private readonly IRepositoryUser userrepository;
    private readonly IHashService _hashService;
    private readonly IShopService _shopService;

    public UserServer(IRepositoryUser userrepository, IHashService hashService, IShopService shopService)
    {
        this.userrepository = userrepository;
        this._hashService = hashService;
        this._shopService = shopService;
    }
    public Page<User> GetUsers()
    {
        return userrepository.GetPage(userrepository.DbSet(), 1, 20);
    }

    public User? GetUser(Guid id)
    {
        var userid = userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        return userid;
    }

    public User EditUser(User user)
    {
        var fromdb = GetUser(user.Id);
        if (fromdb == null)
        {
            throw new UserNotFoundException();
        }

        fromdb.Name = user.Name;
        fromdb.Surname = user.Surname;
        fromdb.Patronymic = user.Patronymic;
        userrepository.Save();
        return fromdb;
    }

    public User CreateAdmin(User user)
    {
        if (userrepository.GetUser(user.Email) != null)
        {
            throw new EmailException();
        }

        user.Password = _hashService.Hash(user.Password);
        userrepository.Create(user);
        userrepository.Save();
        return user;
    }

    public User ChangeBlockUser(Guid id, bool value)
    {
        var userid = userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        userid.IsActive = value;
        userrepository.Save();
        return userid;
    }

    public List<Shop> GetFavoriteShops(Guid userid)
    {
        var user = userrepository.GetById(userid);
        return user.FavoriteShops;
    }

    public Shop CreateFavShop(Guid shopid, Guid userid)
    {
        var user = userrepository.GetById(userid);
        var shop = _shopService.GetShop(shopid);
        user.FavoriteShops.Add(shop);
        userrepository.Save();
        return shop;
    }
    public Shop DelFavShop(Guid shopid, Guid userid)
    {
        var user = userrepository.GetById(userid);
        var shop = _shopService.GetShop(shopid);
        user.FavoriteShops.Remove(shop);
        userrepository.Save();
        return shop;
    }
}