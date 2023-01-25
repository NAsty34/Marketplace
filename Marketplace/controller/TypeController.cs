using AutoMapper;
using logic.Service.Inreface;
using Marketplace.DTO;
using Type = data.model.Type;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Route("/api/v1/type")]
public class TypeController:BaseController<Type, TypeDTO>
{
    public TypeController(ITypeService _base, IMapper mapper) : base(_base, mapper)
    {
    }
}