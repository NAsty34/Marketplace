using System.Text;
using data;
using data.model;
using data.Repository;
using data.Repository.Interface;
using logic;
using logic.Exceptions;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.controller;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Configuration.AddJsonFile("Config.json");
var appConfig = builder.Configuration;
builder.Services.AddSingleton<DBContext>();
builder.Services.AddSingleton<IRepositoryUser, UserRepository>();
builder.Services.AddSingleton<IFeedbackRepositiry, FeedbackRepositoty>();
builder.Services.AddSingleton<IShopRepository, ShopRepository>();
builder.Services.AddSingleton<IAuthService, AuthServer>();
builder.Services.AddSingleton<IJWTService, JWTService>();
builder.Services.AddSingleton<IHashService, HashService>();
builder.Services.AddSingleton<ISendEmailService, SendEmailService>();
builder.Services.AddSingleton<IUserServer, UserServer>();
builder.Services.AddSingleton<IShopService, ShopService>();
builder.Services.AddSingleton<IFeedbackService, FeedbackService>();
builder.Services.AddAuthorization();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = appConfig["ISSUER"],
            ValidateAudience = true,
            ValidAudience = appConfig["AUDIENCE"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfig["KEY"])),
            ValidateIssuerSigningKey = true,
        };
    });
var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthMiddleware>();

app.MapControllerRoute(name: "default", pattern: "/");

app.Run();

