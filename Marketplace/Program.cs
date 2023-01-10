using System.Text;
using data;
using data.model;
using data.Repository;
using data.Repository.Interface;
using logic;
using logic.Exceptions;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

//new(Encoding.UTF8.GetBytes(KEY))

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Configuration.AddJsonFile("Config.json");
builder.Services.AddSingleton<DBContext>(new DBContext());
builder.Services.AddSingleton<IRepositoryUser, UserRepository>();
builder.Services.AddSingleton<IFeedbackRepositiry, FeedbackRepositoty>();
builder.Services.AddSingleton<IShopRepository, ShopRepository>();
builder.Services.AddSingleton<IAuthService, AuthServer>();
builder.Services.AddSingleton<IUserServer, UserServer>();
builder.Services.AddSingleton<IShopService, ShopService>();
builder.Services.AddSingleton<IFeedbackService, FeedbackService>();
builder.Services.AddAuthorization();

var appConfig = builder.Configuration;

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
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    ResponceDto<string> response;
    if (exception is BaseException)
    {
        response = new ResponceDto<string>(exception.Message, (exception as BaseException).Code);
    }
    else
    {
        response = new ResponceDto<string>(exception.Message, 1);
    }
    await context.Response.WriteAsJsonAsync(response);
}));
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "/");

app.Run();

