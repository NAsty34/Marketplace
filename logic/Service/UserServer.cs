using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class UserServer:IUserServer
{
    private readonly IRepositoryUser _userrepository;
    private readonly IHashService _hashService;
    private readonly IShopService _shopService;

    public UserServer(IRepositoryUser userrepository, IHashService hashService, IShopService shopService)
    {
        _userrepository = userrepository;
        _hashService = hashService;
        _shopService = shopService;
    }
    public async Task<Page<User>> GetUsers(int? page, int? size)
    {
        return await _userrepository.GetPage(page, size);
    }

    public async Task<User> GetUser(Guid id)
    {
        var userid = await _userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        return userid;
    }

    public async Task<User> EditUser(User user)
    {
        var fromdb = await GetUser(user.Id);
        if (fromdb == null)
        {
            throw new UserNotFoundException();
        }

        fromdb.Name = user.Name;
        fromdb.Surname = user.Surname;
        fromdb.Patronymic = user.Patronymic;
        await _userrepository.Save();
        return fromdb;
    }

    public async void CreateAdmin(User user)
    {
        if (_userrepository.GetUser(user.Email!) != null)
        {
            throw new EmailException();
        }

        user.Password = _hashService.Hash(user.Password);
        await _userrepository.Create(user);
        await _userrepository.Save();
        
    }

    public async Task<User> ChangeBlockUser(Guid id, bool value)
    {
        var userid = await _userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        userid.IsActive = value;
        await _userrepository.Save();
        return userid;
    }

    public async Task<List<Shop>> GetFavoriteShops(Guid userid)
    {
        var user = await _userrepository.GetById(userid);
        return user.FavoriteShops;
    }

    public async Task<Shop> CreateFavShop(Guid shopid, Guid userid)
    {
        var user = await _userrepository.GetById(userid);
        var shop = await _shopService.GetShop(shopid);
        user.FavoriteShops.Add(shop);
        await _userrepository.Save();
        return shop;
    }
    public async Task<Shop> DelFavShop(Guid shopid, Guid userid)
    {
        var user = await _userrepository.GetById(userid);
        var shop = await _shopService.GetShop(shopid);
        user.FavoriteShops.Remove(shop);
        await _userrepository.Save();
        return shop;
    }
}