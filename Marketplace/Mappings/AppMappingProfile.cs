using AutoMapper;
using data.model;
using Marketplace.DTO;
using Type = data.model.Type;

namespace Marketplace.Mappings;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Shop, ShopDTO>();
        CreateMap<Shop, ShopDTO>().ReverseMap();
        
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Page<Category>, Page<CategoryDto>>();
        CreateMap<Page<Category>, Page<CategoryDto>>().ReverseMap();
        
        CreateMap<DeliveryType, DeliveryTypeDTO>();
        CreateMap<DeliveryType, DeliveryTypeDTO>().ReverseMap();
        CreateMap<Page<DeliveryType>, Page<DeliveryTypeDTO>>();
        CreateMap<Page<DeliveryType>, Page<DeliveryTypeDTO>>().ReverseMap();
        
        CreateMap<PaymentMethod, PaymentMethodDTO>();
        CreateMap<PaymentMethod, PaymentMethodDTO>().ReverseMap();
        CreateMap<Page<PaymentMethod>, Page<PaymentMethodDTO>>();
        CreateMap<Page<PaymentMethod>, Page<PaymentMethodDTO>>().ReverseMap(); 
        
        CreateMap<Type, TypeDTO>();
        CreateMap<Type, TypeDTO>().ReverseMap();
        CreateMap<Page<Type>, Page<TypeDTO>>();
        CreateMap<Page<Type>, Page<TypeDTO>>().ReverseMap();
    }
}