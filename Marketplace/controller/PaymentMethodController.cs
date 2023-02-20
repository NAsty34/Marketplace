using AutoMapper;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Authorize]
[Route("/api/v1/paymentmethod")]
public class PaymentMethodController:BaseController<PaymentMethod, PaymentMethodDto>
{
    public PaymentMethodController(IBaseService<PaymentMethod> @base, IMapper mapper) : base(@base, mapper)
    {
    }
}