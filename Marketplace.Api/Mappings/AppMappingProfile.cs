using AutoMapper;
using data.model;
using Marketplace.DTO;

namespace Marketplace.Mappings;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<ShopEntity, ShopDto>();
        CreateMap<ShopEntity, ShopDto>().ReverseMap();
        
        CreateMap<ProductEntity, ProductDto>();
        CreateMap<ProductEntity, ProductDto>().ReverseMap();
        
        CreateMap<CategoryEntity, CategoryDto>();
        CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
        CreateMap<PageEntity<CategoryEntity>, PageEntity<CategoryDto>>();
        CreateMap<PageEntity<CategoryEntity>, PageEntity<CategoryDto>>().ReverseMap();
        
        CreateMap<DeliveryTypeEntity, DeliveryTypeDto>();
        CreateMap<DeliveryTypeEntity, DeliveryTypeDto>().ReverseMap();
        CreateMap<PageEntity<DeliveryTypeEntity>, PageEntity<DeliveryTypeDto>>();
        CreateMap<PageEntity<DeliveryTypeEntity>, PageEntity<DeliveryTypeDto>>().ReverseMap();
        
        CreateMap<PaymentMethodEntity, PaymentMethodDto>();
        CreateMap<PaymentMethodEntity, PaymentMethodDto>().ReverseMap();
        CreateMap<PageEntity<PaymentMethodEntity>, PageEntity<PaymentMethodDto>>();
        CreateMap<PageEntity<PaymentMethodEntity>, PageEntity<PaymentMethodDto>>().ReverseMap(); 
        
        CreateMap<TypeEntity, TypeDto>();
        CreateMap<TypeEntity, TypeDto>().ReverseMap();
        CreateMap<PageEntity<TypeEntity>, PageEntity<TypeDto>>();
        CreateMap<PageEntity<TypeEntity>, PageEntity<TypeDto>>().ReverseMap();
    }
}