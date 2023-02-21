using AutoMapper;
using data.model;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Authorize]
[Route("/api/v1/categories")]
public class CategoryController:BaseController<Category, CategoryDto>
{
    
    public CategoryController(IBaseService<Category> @base, IMapper mapper) : base(@base, mapper)
    {
    }
    
}