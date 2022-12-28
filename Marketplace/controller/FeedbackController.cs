using Microsoft.AspNetCore.Mvc;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.controller;
[Authorize]
public class FeedbackController:Controller
{
    [Route("/api/v1/users/{id}/feedback")]
    [HttpPost]
    public ResponceDto<string> UserFeedback() => new("GET /api/v1/users/{id}/feedback");

    [Route("/api/v1/shops/{id}/feedback")]
    [HttpPost]
    public ResponceDto<string> ShopFeedback() => new("GET /api/v1/shops/{id}/feedback");
    
    [Route("/api/v1/shops/{id}/feedback")]
    [HttpPost]
    public ResponceDto<string> AddFeedback() => new("POST /api/v1/shops/{id}/feedback");
    
    [Route("/api/v1/shops/{shopid}/feedback/{id}")]
    [HttpPost]
    public ResponceDto<string> EditFeedback() => new("PUT /api/v1/shops/{id}/feedback/{id}");

    [Route("/api/v1/shops/{shopid}/feedback/{id}")]
    [HttpPost]
    public ResponceDto<string> DeleteFeedback() => new("DELETE /api/v1/shops/{id}/feedback/{id}");
}