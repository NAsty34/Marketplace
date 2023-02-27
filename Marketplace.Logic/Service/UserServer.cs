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
    public async Task<PageEntity<UserEntity>> GetUsers(int? page, int? size)
    {
        return await _userrepository.GetPage(page, size);
    }

    public async Task<UserEntity> GetUser(Guid id)
    {
        var userid = await _userrepository.GetById(id);
        if (userid == null)
        {
            throw new UserNotFoundException();
        }

        return userid;
    }

    public async Task<UserEntity> EditUser(UserEntity userEntity)
    {
        var fromdb = await GetUser(userEntity.Id);
        if (fromdb == null)
        {
            throw new UserNotFoundException();
        }

        fromdb.Name = userEntity.Name;
        fromdb.Surname = userEntity.Surname;
        fromdb.Patronymic = userEntity.Patronymic;
        await _userrepository.Save();
        return fromdb;
    }

    public async Task CreateAdmin(UserEntity userEntity)
    {
        if (_userrepository.GetUser(userEntity.Email!) != null)
        {
            throw new EmailException();
        }

        userEntity.Password = _hashService.Hash(userEntity.Password);
        await _userrepository.Create(userEntity);
        await _userrepository.Save();
        
    }

    public async Task<UserEntity> ChangeBlockUser(Guid id, bool value)
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
    public async Task<List<ShopEntity>> GetFavoriteShops(Guid userid)
    {
        var user = await _userrepository.GetById(userid);
        return user.FavoriteShops;
    }

    public async Task<ShopEntity> CreateFavShop(Guid shopid, Guid userid)
    {
        var user = await _userrepository.GetById(userid);
        var shop = await _shopService.GetShop(shopid);
        if (shop == null || !shop.IsActive) throw new ShopNotFoundException();
        user.FavoriteShops.Add(shop);
        await _userrepository.Save();
        return shop;
    }
    public async Task<ShopEntity> DelFavShop(Guid shopid, Guid userid)
    {
        var user = await _userrepository.GetById(userid);
        var shop = await _shopService.GetShop(shopid);
        if (shop == null || !shop.IsActive) throw new ShopNotFoundException();
        user.FavoriteShops.Remove(shop);
        await _userrepository.Save();
        return shop;
    }
}