using AutoMapper;
using data.model;
using Marketplace.DTO;

namespace Marketplace.Mappings;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Shop, ShopDto>();
        CreateMap<Shop, ShopDto>().ReverseMap();
        
        CreateMap<ProductEntity, ProductDto>();
        CreateMap<ProductEntity, ProductDto>().ReverseMap();
        
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Page<Category>, Page<CategoryDto>>();
        CreateMap<Page<Category>, Page<CategoryDto>>().ReverseMap();
        
        CreateMap<DeliveryType, DeliveryTypeDto>();
        CreateMap<DeliveryType, DeliveryTypeDto>().ReverseMap();
        CreateMap<Page<DeliveryType>, Page<DeliveryTypeDto>>();
        CreateMap<Page<DeliveryType>, Page<DeliveryTypeDto>>().ReverseMap();
        
        CreateMap<PaymentMethod, PaymentMethodDto>();
        CreateMap<PaymentMethod, PaymentMethodDto>().ReverseMap();
        CreateMap<Page<PaymentMethod>, Page<PaymentMethodDto>>();
        CreateMap<Page<PaymentMethod>, Page<PaymentMethodDto>>().ReverseMap(); 
        
        CreateMap<TypeEntity, TypeDto>();
        CreateMap<TypeEntity, TypeDto>().ReverseMap();
        CreateMap<Page<TypeEntity>, Page<TypeDto>>();
        CreateMap<Page<TypeEntity>, Page<TypeDto>>().ReverseMap();
    }
}