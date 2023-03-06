using FreeProduct.Services.Catalog.DTOs;
using FreeProduct.Services.Catalog.Services;
using FreeProduct.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Token� kimin da��tt��� url bilgisini veriyoruz.
    options.Authority = builder.Configuration.GetSection("IdentityServerURL").Value;
    options.Audience = "resource_catalog"; // Audience parametresi ile bir json web token yap�ld���nda buradaki isme bakacak ve gelen token�n parametresi ile kar��la�t�racak.    
    options.RequireHttpsMetadata = false; // https kullanmad���m�z� belirtiyoruz.
});

builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

var dbs = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();
builder.Services.AddSingleton<IDatabaseSettings>(sp => { return dbs; });

//builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseStrings"));

//builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
//builder.Services.AddSingleton<IDatabaseSettings>(sp =>
//{
//return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
//});


builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region SEED DATA
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var categoryService = serviceProvider.GetRequiredService<ICategoryService>();

    if (!categoryService.GetAllAsync().Result.Data.Any())
    {
        categoryService.CreateAsync(new CategoryDto { Name = "Asp.Net Core Learning Book" }).Wait();
        categoryService.CreateAsync(new CategoryDto { Name = "C# 101 Book" }).Wait();
    }
}
#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
