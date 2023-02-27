using data.model;
using data.Repository.Interface;
using logic.Exceptions;
using logic.Service.Inreface;

namespace logic.Service;

public class ShopService : IShopService
{
    private IShopRepository _shopRepository;

    //private IRepositoryUser _repositoryUser;
    private IShopDictionaryRepository<ShopCategoryEntity> _categoriesShop;
    private IShopDictionaryRepository<ShopDeliveryEntity> _deliveryShop;
    private IShopDictionaryRepository<ShopPaymentEntity> _paymentShop;
    private IShopDictionaryRepository<ShopTypesEntity> _typeShop;
    private IBaseRopository<CategoryEntity> _caropository;
    private IBaseRopository<TypeEntity> _taropository;
    private IBaseRopository<PaymentMethodEntity> _paRopository;
    private IBaseRopository<DeliveryTypeEntity> _daRopository;

    public ShopService(IShopRepository ishoprepository, IShopDictionaryRepository<ShopCategoryEntity> categoriesShop,
        IShopDictionaryRepository<ShopDeliveryEntity> deliveryShop,
        IShopDictionaryRepository<ShopPaymentEntity> paymentShop, IShopDictionaryRepository<ShopTypesEntity> typeShop,
        IBaseRopository<CategoryEntity> caropository, IBaseRopository<TypeEntity> taropository,
        IBaseRopository<PaymentMethodEntity> paRopository, IBaseRopository<DeliveryTypeEntity> daRopository)
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

    public async Task<PageEntity<ShopEntity>> GetShops(FiltersShopsEntity filtersShopsEntity, int? page, int? size)
    {
        return await _shopRepository.GetPage(filtersShopsEntity, page, size);
    }

    public async Task<ShopEntity> GetShop(Guid id)
    {
        var shopid = await _shopRepository.GetById(id);
        if (shopid == null || !shopid.IsActive)
        {
            throw new ShopNotFoundException();
        }

        return shopid;
    }

    public async Task DeleteShop(Guid id)
    {
        var shopId = await _shopRepository.GetById(id);
        if (shopId == null || !shopId.IsActive) throw new ShopNotFoundException();

        await _categoriesShop.DeleteAllByShop(id);
        await _deliveryShop.DeleteAllByShop(id);
        await _paymentShop.DeleteAllByShop(id);
        await _typeShop.DeleteAllByShop(id);
        await _shopRepository.Delete(shopId);
        await _shopRepository.Save();
    }

    public async Task<ShopEntity> CreateShop(ShopEntity shopEntity)
    {
        if (await _shopRepository.GetByInn(shopEntity.Inn) != null)
        {
            throw new InnAlreadyUseException();
        }

        await Checkfield(shopEntity);

        /*var api = new SuggestClientAsync(appConfig["token"]);
        var result = await api.FindParty(shop.Inn);
        
        if (result.suggestions.Count == 0)
        {
            throw new InnIncorrectException();
        }*/


        await _shopRepository.Create(shopEntity);
        await _shopRepository.Save();
        return shopEntity;
    }


    public async Task<ShopEntity> EditShop(ShopEntity shopEntity, Guid userid, RoleEntity roleEntity)
    {
        var fromDb = await _shopRepository.GetById(shopEntity.Id);
        if (fromDb == null || !fromDb.IsActive)
        {
            throw new ShopNotFoundException();
        }

        if (roleEntity != RoleEntity.Admin && fromDb.CreatorId != userid)
        {
            throw new AccessDeniedException();
        }

        fromDb.Description = shopEntity.Description;
        fromDb.Logo = shopEntity.Logo;
        fromDb.Inn = shopEntity.Inn;
        fromDb.Name = shopEntity.Name;
        fromDb.IsPublic = shopEntity.IsPublic;

        await _categoriesShop.DeleteAllByShop(shopEntity.Id);
        await _deliveryShop.DeleteAllByShop(shopEntity.Id);
        await _paymentShop.DeleteAllByShop(shopEntity.Id);
        await _typeShop.DeleteAllByShop(shopEntity.Id);

        await Checkfield(shopEntity);

        fromDb.ShopCategory = shopEntity.ShopCategory;
        fromDb.ShopDeliveries = shopEntity.ShopDeliveries;
        fromDb.ShopPayment = shopEntity.ShopPayment;
        fromDb.ShopTypes = shopEntity.ShopTypes;


        await _shopRepository.Save();
        return fromDb;
    }

    public async Task<ShopEntity> ChangeBlockShop(Guid id, bool value)
    {
        var shopid = await _shopRepository.GetById(id);
        if (shopid is null)
        {
            throw new ShopNotFoundException();
        }

        shopid.IsActive = value;
        await _shopRepository.Save();
        return shopid;
    }

    public async Task Checkfield(ShopEntity shopEntity)
    {
        var idsCategory = shopEntity.ShopCategory.Select(a => a.CategoryId);
        var cids = await _caropository.GetByIds(idsCategory);
        if (cids.Count() != idsCategory.Count() || cids.Select(a => !a.IsDeleted).Equals(idsCategory))
        {
            throw new CategoryNotFoundException();
        }

        var idsType = shopEntity.ShopTypes.Select(a => a.TypeId);
        var tids = await _taropository.GetByIds(idsType);
        if (tids.Count() != idsType.Count() || tids.Select(a => !a.IsDeleted).Equals(idsType))
        {
            throw new TypeNotFoundException();
        }

        var idspayment = shopEntity.ShopPayment.Select(a => a.PaymentId);
        var idpayment = await _paRopository.GetByIds(idspayment);
        var paymentList = idpayment.ToList();
        var spList = shopEntity.ShopPayment.ToList();


        for (int i = 0; i < paymentList.Count(); i++)
        {
            if (!paymentList[i].Commission) spList[i].Commission = 0;
            if (spList[i].Commission > 1 ||
                spList[i].Commission < 0 && idpayment.Select(a => !a.IsDeleted).Equals(idspayment))
            {
                throw new CommissionNotUseException();
            }
        }

        if (idpayment.Count() != idspayment.Count())
        {
            throw new PyementNotFoundException();
        }

        var idsdelivery = shopEntity.ShopDeliveries.Select((a => a.DeliveryId));
        var dids = await _daRopository.GetByIds(idsdelivery);
        if (dids.Count() != idsdelivery.Count() || dids.Select(a => !a.IsDeleted).Equals(idsdelivery))
        {
            throw new DeliveryNotFoundException();
        }

        var deliverylist = await _daRopository.GetByIds(idsdelivery);
        var delivList = deliverylist.ToList();
        var delist = shopEntity.ShopDeliveries.ToList();
        for (int i = 0; i < delivList.Count(); i++)
        {
            if (!delivList[i].Free) delist[i].Price = 0;
        }
    }
}