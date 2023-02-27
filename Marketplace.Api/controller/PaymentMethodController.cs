using AutoMapper;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Authorize]
[Route("/api/v1/paymentmethod")]
public class PaymentMethodController:BaseController<PaymentMethodEntity, PaymentMethodDto>
{
    public PaymentMethodController(IBaseService<PaymentMethodEntity> @base, IMapper mapper) : base(@base, mapper)
    {
    }
}