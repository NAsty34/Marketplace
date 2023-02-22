using System.Text;
using data;
using data.model;
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

builder.Configuration.AddJsonFile("appsettings.Production.json");
//builder.Services.Configure<ImagesConfiguration>(builder.Configuration.GetSection("Images"));
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

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
builder.Services.AddTransient <IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IShopDictionaryRepository<>), typeof(ShopDictionaryRepository<>));

builder.Services.AddScoped(typeof(IBaseRopository<>), typeof(BaseRepository<>));
builder.Services.AddScoped <IBaseRopository<Category>, CategoryRepository>();

builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped <IBaseService<Category>, CategoryService>();
builder.Services.AddScoped <IBaseService<TypeEntity>, TypeService>();



builder.Services.AddTransient <IAuthService, AuthServer>();
builder.Services.AddTransient <IJwtService, JwtService>();
builder.Services.AddTransient <IHashService, HashService>();
builder.Services.AddTransient <ISendEmailService, SendEmailService>();
builder.Services.AddTransient <IUserServer, UserServer>();
builder.Services.AddTransient <IShopService, ShopService>();
builder.Services.AddTransient <IFeedbackService, FeedbackService>();
builder.Services.AddTransient <IFileInfoService, FileInfoService>();
builder.Services.AddTransient <IProductService, ProductService>();

builder.Services.AddAuthorization();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = new JwtTokenOptions();
        appConfig.GetSection(JwtTokenOptions.JwtToken).Bind(jwt);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
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

