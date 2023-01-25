using AutoMapper;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Mvc;


namespace Marketplace.controller;
[Route("/api/v1/delivery")]
public class DeliveryTypeController:BaseController<DeliveryType, DeliveryTypeDTO>
{
    public DeliveryTypeController(IDeliveryTypeService _base, IMapper mapper) : base(_base, mapper)
    {
    }

}