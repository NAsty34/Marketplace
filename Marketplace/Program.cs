using data.model;
using data.Repository;
using logic;
using logic.Service;
using Marketplace.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddTransient<IRepositoryUser, UserRepository>();
builder.Services.AddTransient<IFeedbackRepositiry, FeedbackRepositoty>();
builder.Services.AddTransient<IAuthService, AuthServer>();
builder.Services.AddTransient<IUserServer, UserServer>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
var app = builder.Build();
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
    var response = new ResponceDto<string>(exception.Message, false);
    await context.Response.WriteAsJsonAsync(response);
}));
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "/");

app.Run();

