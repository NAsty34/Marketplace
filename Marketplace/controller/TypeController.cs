using AutoMapper;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Authorize]
[Route("/api/v1/type")]
public class TypeController:BaseController<TypeEntity, TypeDto>
{
    public TypeController(IBaseService<TypeEntity> @base, IMapper mapper) : base(@base, mapper)
    {
    }

}