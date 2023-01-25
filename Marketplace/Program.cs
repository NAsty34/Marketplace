using System.Text;
using data;
using data.Repository;
using data.Repository.Interface;
using logic.Service;
using logic.Service.Inreface;
using Marketplace.controller;
using Marketplace.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Configuration.AddJsonFile("Config.json");
var appConfig = builder.Configuration;
builder.Services.AddSingleton<DBContext>();

builder.Services.AddDbContext<DBContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddSingleton<IRepositoryUser, UserRepository>();
builder.Services.AddSingleton<IFeedbackRepositiry, FeedbackRepositoty>();
builder.Services.AddSingleton<IShopRepository, ShopRepository>();
builder.Services.AddSingleton<IFileInfoRepository, FileInfoRepository>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ITypeRepository, TypeRepository>();
builder.Services.AddSingleton<IDeliveryTypeRepository, DeliveryTypeRepository>();
builder.Services.AddSingleton<IPaymentMethodRepository, PaymentMethodRepository>();

builder.Services.AddSingleton<IAuthService, AuthServer>();
builder.Services.AddSingleton<IJWTService, JWTService>();
builder.Services.AddSingleton<IHashService, HashService>();
builder.Services.AddSingleton<ISendEmailService, SendEmailService>();
builder.Services.AddSingleton<IUserServer, UserServer>();
builder.Services.AddSingleton<IShopService, ShopService>();
builder.Services.AddSingleton<IFeedbackService, FeedbackService>();
builder.Services.AddSingleton<IFileInfoService, FileInfoService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IDeliveryTypeService, DeliveryTypeService>();
builder.Services.AddSingleton<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddSingleton<ITypeService, TypeService>();

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
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"files")),
    RequestPath = new PathString("/static")
});
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DBContext>();
    db.Database.Migrate();
}

app.MapControllerRoute(name: "default", pattern: "/");

app.Run();

