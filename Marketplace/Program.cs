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
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Configuration.AddJsonFile("appsettings.Production.json");
var appConfig = builder.Configuration;


builder.Services.AddDbContext<DBContext>(option =>
{
    option.UseLazyLoadingProxies();
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    option.UseNpgsql(appConfig["ConnectionString"]);
});

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "Aaaaa",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

builder.Services.AddTransient <IRepositoryUser, UserRepository>();
builder.Services.AddTransient <IFeedbackRepositiry, FeedbackRepositoty>();
builder.Services.AddTransient <IShopRepository, ShopRepository>();
builder.Services.AddTransient <IFileInfoRepository, FileInfoRepository>();
builder.Services.AddTransient <ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient <ITypeRepository, TypeRepository>();
builder.Services.AddTransient <IDeliveryTypeRepository, DeliveryTypeRepository>();
builder.Services.AddTransient <IPaymentMethodRepository, PaymentMethodRepository>();

builder.Services.AddTransient <IAuthService, AuthServer>();
builder.Services.AddTransient <IJWTService, JWTService>();
builder.Services.AddTransient <IHashService, HashService>();
builder.Services.AddTransient <ISendEmailService, SendEmailService>();
builder.Services.AddTransient <IUserServer, UserServer>();
builder.Services.AddTransient <IShopService, ShopService>();
builder.Services.AddTransient <IFeedbackService, FeedbackService>();
builder.Services.AddTransient <IFileInfoService, FileInfoService>();
builder.Services.AddTransient <ICategoryService, CategoryService>();
builder.Services.AddTransient <IDeliveryTypeService, DeliveryTypeService>();
builder.Services.AddTransient <IPaymentMethodService, PaymentMethodService>();
builder.Services.AddTransient <ITypeService, TypeService>();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


/*using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DBContext>();
    db.Database.Migrate();
}*/

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"files")),
    RequestPath = new PathString("/static")
});


app.MapControllerRoute(name: "default", pattern: "/");

app.Run();

