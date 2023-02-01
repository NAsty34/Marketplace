using AutoMapper;
using data.model;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Authorize]
[Route("/api/v1/paymentmethod")]
public class PaymentMethodController:BaseController<PaymentMethod, PaymentMethodDTO>
{
    public PaymentMethodController(IBaseService<PaymentMethod> _base, IMapper mapper) : base(_base, mapper)
    {
    }
}