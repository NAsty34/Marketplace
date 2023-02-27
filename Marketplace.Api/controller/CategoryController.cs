using AutoMapper;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Authorize]
[Route("/api/v1/categories")]
public class CategoryController:BaseController<CategoryEntity, CategoryDto>
{
    
    public CategoryController(IBaseService<CategoryEntity> @base, IMapper mapper) : base(@base, mapper)
    {
    }
    
}