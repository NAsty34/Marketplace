using AutoMapper;
using data.model;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Marketplace.controller;
[Authorize]
[Route("/api/v1/delivery")]
public class DeliveryTypeController:BaseController<DeliveryType, DeliveryTypeDTO>
{
    public DeliveryTypeController(IBaseService<DeliveryType> _base, IMapper mapper) : base(_base, mapper)
    {
    }

}