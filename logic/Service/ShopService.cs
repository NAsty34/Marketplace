using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class ShopService:IShopService
{

    private IShopRepository _shopRepository;
    //private IRepositoryUser _repositoryUser;
    private IShopDictionaryRepository<ShopCategory> _categoriesShop;
    private IShopDictionaryRepository<ShopDelivery> _deliveryShop;
    private IShopDictionaryRepository<ShopPayment> _paymentShop;
    private IShopDictionaryRepository<ShopTypes> _typeShop;
    private IBaseRopository<Category> _caropository;
    private IBaseRopository<TypeEntity> _taropository;
    private IBaseRopository<PaymentMethod> _paRopository;
    private IBaseRopository<DeliveryType> _daRopository;
    
    public ShopService(IShopRepository ishoprepository, IShopDictionaryRepository<ShopCategory> categoriesShop, IShopDictionaryRepository<ShopDelivery> deliveryShop, IShopDictionaryRepository<ShopPayment> paymentShop, IShopDictionaryRepository<ShopTypes> typeShop, 
        IBaseRopository<Category> caropository, IBaseRopository<TypeEntity> taropository, IBaseRopository<PaymentMethod> paRopository, IBaseRopository<DeliveryType> daRopository)
    {
        _shopRepository = ishoprepository;
        _categoriesShop = categoriesShop;
        _deliveryShop = deliveryShop;
        _paymentShop = paymentShop;
        _typeShop = typeShop;
        _caropository = caropository;
        _taropository = taropository;
        _paRopository = paRopository;
        _daRopository = daRopository;
    }
    
    public async Task<Page<Shop>> GetShops(FiltersShops filtersShops)
    {
        return await _shopRepository.GetPage(filtersShops);
    }

    public async Task<Shop> GetShop(Guid id)
    {
        var shopid = await _shopRepository.GetById(id);
        if (shopid == null)
        {
            throw new ShopNotFoundException();
        }

        return shopid;
    }

    public async Task DeleteShop(Guid id)
    {
        var shopid = await _shopRepository.GetById(id);
        if (shopid != null)
        {
            await _categoriesShop.DeleteAllByShop(id);
            await _deliveryShop.DeleteAllByShop(id);
            await _paymentShop.DeleteAllByShop(id);
            await _typeShop.DeleteAllByShop(id);
            _shopRepository.Delete(shopid);
            await _shopRepository.Save();
        }
        
    }

    public async Task<Shop> CreateShop(Shop shop)
    {
        if (await _shopRepository.GetByInn(shop.Inn) != null)
        {
            throw new InnAlreadyUseException();
        }
        var idsCategory = shop.ShopCategory.Select(a => a.CategoryId);
        var cids = await _caropository.GetByIds(idsCategory); 
        if (cids.Count() != idsCategory.Count())
        {
            throw new CategoryNotFoundException();
        }

        var idsType = shop.ShopTypes.Select(a => a.TypeId);
        var tids = await _taropository.GetByIds(idsType); 
        if (tids.Count() != idsType.Count())
        {
            throw new TypeNotFoundException();
        }

        var idspayment = shop.ShopPayment.Select(a => a.PaymentId);
        var idpayment = await _paRopository.GetByIds(idspayment);
        var paymentList = idpayment.ToList();
        var spList = shop.ShopPayment.ToList();
        
        for (int i = 0; i < paymentList.Count(); i++)
        {
            if (!paymentList[i].Commission) spList[i].Commission = 0;
            if (spList[i].Commission > 1 || spList[i].Commission < 0)
            {
                throw new CommissionNotUseException();
            }
        }
        
        if (idpayment.Count() != idspayment.Count())
        {
            throw new PyementNotFoundException();
        }

        var idsdelivery = shop.ShopDeliveries.Select((a => a.DeliveryId));
        var dids = await _daRopository.GetByIds(idsdelivery); 
        if (dids.Count() != idsdelivery.Count())
        {
            throw new DeliveryNotFoundException();
        }

        var deliverylist = await _daRopository.GetByIds(idsdelivery);
        var delivList = deliverylist.ToList();
        var delist = shop.ShopDeliveries.ToList();
        for (int i = 0; i < delivList.Count(); i++)
        {
            if (!delivList[i].Free) delist[i].Price = 0;
        }
     
        /*var api = new SuggestClientAsync(appConfig["token"]);
        var result = await api.FindParty(shop.Inn);
        
        if (result.suggestions.Count == 0)
        {
            throw new InnIncorrectException();
        }*/
      
        
        await _shopRepository.Create(shop);
        await _shopRepository.Save();
        return shop;
    }

   

    public async Task<Shop> EditShop(Shop shop, Guid userid, Role role)
    {
        var fromDb = await _shopRepository.GetById(shop.Id);
        if (fromDb == null)
        {
            throw new ShopNotFoundException();
        }

        if (role != Role.Admin && fromDb.CreatorId != userid)
        {
            throw new AccessDeniedException();
        }

        fromDb.Description = shop.Description;
        fromDb.Logo = shop.Logo;
        fromDb.Inn = shop.Inn;
        fromDb.Name = shop.Name;
        fromDb.IsPublic = shop.IsPublic;

        await _categoriesShop.DeleteAllByShop(shop.Id);
        await _deliveryShop.DeleteAllByShop(shop.Id);
        await _paymentShop.DeleteAllByShop(shop.Id);
        await _typeShop.DeleteAllByShop(shop.Id);
        
        var idsCategory = shop.ShopCategory.Select(a => a.CategoryId);
        var cids = await _caropository.GetByIds(idsCategory); 
        if (cids.Count() != idsCategory.Count())
        {
            throw new CategoryNotFoundException();
        }

        var idsType = shop.ShopTypes.Select(a => a.TypeId);
        var tids = await _taropository.GetByIds(idsType); 
        if (tids.Count() != idsType.Count())
        {
            throw new TypeNotFoundException();
        }

        var idspayment = shop.ShopPayment.Select(a => a.PaymentId);
        var idpayment = await _paRopository.GetByIds(idspayment);
        var paymentList = idpayment.ToList();
        var spList = shop.ShopPayment.ToList();
        
        for (int i = 0; i < paymentList.Count(); i++)
        {
            if (!paymentList[i].Commission) spList[i].Commission = 0;
            if (spList[i].Commission > 1 || spList[i].Commission < 0)
            {
                throw new CommissionNotUseException();
            }
        }
        
        if (idpayment.Count() != idspayment.Count())
        {
            throw new PyementNotFoundException();
        }

        var idsdelivery = shop.ShopDeliveries.Select((a => a.DeliveryId));
        var dids = await _daRopository.GetByIds(idsdelivery); 
        if (dids.Count() != idsdelivery.Count())
        {
            throw new DeliveryNotFoundException();
        }

        var deliverylist = await _daRopository.GetByIds(idsdelivery);
        var delivList = deliverylist.ToList();
        var delist = shop.ShopDeliveries.ToList();
        for (int i = 0; i < delivList.Count(); i++)
        {
            if (!delivList[i].Free) delist[i].Price = 0;
        }
        
        fromDb.ShopCategory = shop.ShopCategory;
        fromDb.ShopDeliveries = shop.ShopDeliveries;
        fromDb.ShopPayment = shop.ShopPayment;
        fromDb.ShopTypes = shop.ShopTypes;
        
       
        await _shopRepository.Save();
        return fromDb;
    }

    public async Task<Shop> ChangeBlockShop(Guid id, bool value)
    {
        var shopid = await _shopRepository.GetById(id);
        if (shopid is null)
        {
            throw new ShopNotFoundException();
        }

        shopid.IsActive = !value;
        await _shopRepository.Save();
        return shopid;
    }


    
}