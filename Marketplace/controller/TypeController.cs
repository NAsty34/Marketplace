using AutoMapper;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Type = data.model.Type;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Authorize]
[Route("/api/v1/type")]
public class TypeController:BaseController<Type, TypeDTO>
{
    public TypeController(IBaseService<Type> _base, IMapper mapper) : base(_base, mapper)
    {
    }
}