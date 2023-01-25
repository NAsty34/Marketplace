using AutoMapper;
using data.model;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.controller;
[Route("/api/v1/categories")]
public class CategoryController:BaseController<Category, CategoryDto>
{
    public CategoryController(ICategoryService _base, IMapper mapper) : base(_base, mapper)
    {
    }
}