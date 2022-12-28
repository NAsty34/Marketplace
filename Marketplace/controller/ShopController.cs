using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;
[Authorize]
public class ShopController:Controller
{
    [Route(" /api/v1/shops")]
    [HttpPost]
    public ResponceDto<string> Shops() => new("GET /api/v1/shops");
    
    [Route("/api/v1/shops/{id}")]
    [HttpPost]
    public ResponceDto<string> GetShops() => new("GET /api/v1/shops/{id}");
    
    [Route("/api/v1/shops")]
    [HttpPost]
    public ResponceDto<string> AdminGetShops() => new("POST /api/v1/shops");
    
    [Route("/api/v1/shops/{id}")]
    [HttpPost]
    public ResponceDto<string> EditGetShops() => new("PUT /api/v1/shops/{id}");
    
    [Route("/api/v1/shops/block/{id}")]
    [HttpPost]
    public ResponceDto<string> BlockGetShops() => new("GET /api/v1/shops/block/{id}");
    
    [Route("/api/v1/shops/unblock/{id}")]
    [HttpPost]
    public ResponceDto<string> UnblockGetShops() => new("GET /api/v1/shops/unblock/{id}");
    
    [Route("/api/v1/shops/{id}")]
    [HttpPost]
    public ResponceDto<string> DeleteShop() => new("Delete /api/v1/shops/{id}");
}