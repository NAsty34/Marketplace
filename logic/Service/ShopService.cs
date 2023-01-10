using Dadata;
using data;
using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.Extensions.Configuration;

namespace logic.Service;

public class ShopService:IShopService
{

    private IShopRepository _shopRepository;
    private IRepositoryUser _repositoryUser;
    private readonly IConfiguration appConfig;
    public ShopService(IShopRepository _ishoprepository, IRepositoryUser _repositoryUser, IConfiguration _appConfig)
    {
        this._shopRepository = _ishoprepository;
        this._repositoryUser = _repositoryUser;
        this.appConfig = _appConfig;
    }
    
    public Page<Shop> GetShops()
    {
        return _shopRepository.GetPage(1, 20);
    }

    public Shop GetShop(int id)
    {
        var s = _shopRepository.GetById(id);
        if (s == null)
        {
            throw new ShopNotFoundException();
        }

        return s;
    }

    public void DeleteShop(int id)
    {
        var s = _shopRepository.GetById(id);
        if (s != null)
        {
            _shopRepository.Delete(s);
            _shopRepository.Save();
        }
        
    }

    public async Task<Shop> CreateShop(Shop shop)
    {
        if (_shopRepository.GetByInn(shop.Inn) != null)
        {
            throw new InnAlreadyUseException();
        }
     
        var api = new SuggestClientAsync(appConfig["token"]);
        var result = await api.FindParty(shop.Inn);
        
        if (result.suggestions.Count == 0)
        {
            throw new InnIncorrectException();
        }
        _shopRepository.Create(shop);
        _shopRepository.Save();
        return shop;
    }

    public Shop EditShop(Shop shop, int userid)
    {
        var FromDB = _shopRepository.GetById(shop.Id);
        if (FromDB == null)
        {
            throw new ShopNotFoundException();
        }

        if (userid>0 && FromDB.CreatorId != userid)
        {
            throw new AccessDeniedException();
        }

        FromDB.Description = shop.Description;
        FromDB.Logo = shop.Logo;
        FromDB.Inn = shop.Inn;
        FromDB.Name = shop.Name;
        FromDB.isPublic = shop.isPublic;
        _shopRepository.Save();
        return FromDB;
    }

    public Page<Shop> GetPublicShops()
    {
        return _shopRepository.GetPublicShops();
    }

    public Page<Shop> GetSellerShops(int id)
    
    {
        var e = _repositoryUser.GetById(id);
        if (e == null)
        {
            throw new UserNotFoundException();
        }
        
        return _shopRepository.GetSellerShops(e.Id);
    }

    public Shop ChangeBlockShop(int id, bool value)
    {
        var s = _shopRepository.GetById(id);
        if (s == null)
        {
            throw new ShopNotFoundException();
        }

        s.IsActive = !value;
        _shopRepository.Save();
        return s;
    }

    
    
}