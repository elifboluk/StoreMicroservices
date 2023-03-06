using FreeProduct.Services.Basket.Services;
using FreeProduct.Services.Basket.Settings;
using FreeProduct.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// Claimler gelirken sub identitynational olarak geliyor, kiþinin id'sini alýrken sub diye aratýyoruz. Bundan dolayý map olayýnda sub'ý devre dýþý býraktýrdýk.
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // Sub'ý mapleme (X)

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{    
    options.Authority = builder.Configuration.GetSection("IdentityServerURL").Value;
    options.Audience = "resource_basket";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(ISharedIdentityService), typeof(SharedIdentityService));
builder.Services.AddScoped(typeof(IBasketService), typeof(BasketService));
// Add services to the container.
var dbs = builder.Configuration.GetSection("RedisSettings").Get<RedisSettings>();

builder.Services.AddSingleton<RedisService>(sp =>
{
    // RedisSettings bilgilerini IOptions interfaceini kullanarak value degerlerini alýyoruz.
    // Var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    // Redis host ve port bilgilerini ekliyorum.
    var redis = new RedisService(dbs.Host, dbs.Port);
    // Connection kuruyorum.
    redis.Connect();

    return redis;
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
