using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Tokenı kimin dağıttığı url bilgisini veriyoruz.
    options.Authority = builder.Configuration.GetSection("IdentityServerURL").Value;
    options.Audience = "resource_photo_stock"; // Audience parametresi ile bir json web token yapıldığında buradaki isme bakacak ve gelen tokenın parametresi ile karşılaştıracak.    
    options.RequireHttpsMetadata = false; // https kullanmadığımızı belirtiyoruz.
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseStaticFiles(); // for image files 

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
