using AutoMapper;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Route("/api/v1/paymentmethod")]
public class PaymentMethodController:BaseController<PaymentMethod, PaymentMethodDTO>
{
    public PaymentMethodController(IPaymentMethodService _base, IMapper mapper) : base(_base, mapper)
    {
    }
}