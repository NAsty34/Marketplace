using Dadata;
using data;
using data.model;
using data.Repository;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace logic.Service;

public class ShopService:IShopService
{

    private IShopRepository _shopRepository;
    private IRepositoryUser _repositoryUser;
    private IShopDictionaryRepository<ShopCategory> CategoriesShop;
    private IShopDictionaryRepository<ShopDelivery> DeliveryShop;
    private IShopDictionaryRepository<ShopPayment> PaymentShop;
    private IShopDictionaryRepository<ShopTypes> TypeShop;
    private readonly IConfiguration appConfig;
    private ILogger<ShopService> _logger;
    public ShopService(IShopRepository _ishoprepository, IRepositoryUser _repositoryUser, IConfiguration _appConfig, IShopDictionaryRepository<ShopCategory> categoriesShop, ILogger<ShopService> _logger, IShopDictionaryRepository<ShopDelivery> deliveryShop, IShopDictionaryRepository<ShopPayment> paymentShop, IShopDictionaryRepository<ShopTypes> typeShop)
    {
        this._shopRepository = _ishoprepository;
        this._repositoryUser = _repositoryUser;
        this.appConfig = _appConfig;
        this.CategoriesShop = categoriesShop;
        this.DeliveryShop = deliveryShop;
        this.PaymentShop = paymentShop;
        this.TypeShop = typeShop;
        this._logger = _logger;
    }
    
    public Page<Shop> GetShops()
    {
        return _shopRepository.GetPage(1, 20);
    }

    public Shop GetShop(Guid id)
    {
        var shopid = _shopRepository.GetById(id);
        if (shopid == null)
        {
            throw new ShopNotFoundException();
        }

        return shopid;
    }

    public void DeleteShop(Guid id)
    {
        var shopid = _shopRepository.GetById(id);
        if (shopid != null)
        {
            CategoriesShop.DeleteAllByShop(id);
            DeliveryShop.DeleteAllByShop(id);
            PaymentShop.DeleteAllByShop(id);
            TypeShop.DeleteAllByShop(id);
            _shopRepository.Delete(shopid);
            _shopRepository.Save();
        }
        
    }

    public void CreateShop(Shop shop)
    {
        if (_shopRepository.GetByInn(shop.Inn) != null)
        {
            throw new InnAlreadyUseException();
        }
     
        /*
        var api = new SuggestClientAsync(appConfig["token"]);
        var result = await api.FindParty(shop.Inn);
        
        if (result.suggestions.Count == 0)
        {
            throw new InnIncorrectException();
        }
        */
        
        _shopRepository.Create(shop);
        _shopRepository.Save();
    }

    public Shop EditShop(Shop shop, Guid userid, Role role)
    {
        var FromDB = _shopRepository.GetById(shop.Id);
        if (FromDB == null)
        {
            throw new ShopNotFoundException();
        }

        if (role != Role.Admin && FromDB.CreatorId != userid)
        {
            throw new AccessDeniedException();
        }

        FromDB.Description = shop.Description;
        FromDB.Logo = shop.Logo;
        FromDB.Inn = shop.Inn;
        FromDB.Name = shop.Name;
        FromDB.isPublic = shop.isPublic;

        CategoriesShop.DeleteAllByShop(shop.Id);
        DeliveryShop.DeleteAllByShop(shop.Id);
        PaymentShop.DeleteAllByShop(shop.Id);
        TypeShop.DeleteAllByShop(shop.Id);
        
        FromDB.ShopCategory = shop.ShopCategory;
        FromDB.ShopDeliveries = shop.ShopDeliveries;
        FromDB.ShopPayment = shop.ShopPayment;
        FromDB.ShopTypes = shop.ShopTypes;
        
       
        _shopRepository.Save();
        return FromDB;
    }

    public Page<Shop> GetPublicShops()
    {
        return _shopRepository.GetPublicShops();
    }

    public Page<Shop> GetSellerShops(Guid id)
    
    {
        var shopid = _repositoryUser.GetById(id);
        if (shopid == null)
        {
            throw new UserNotFoundException();
        }
        
        return _shopRepository.GetSellerShops(shopid.Id);
    }

    public Shop ChangeBlockShop(Guid id, bool value)
    {
        var shopid = _shopRepository.GetById(id);
        if (shopid is null)
        {
            throw new ShopNotFoundException();
        }

        shopid.IsActive = !value;
        _shopRepository.Save();
        return shopid;
    }

    
    
}