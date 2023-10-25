using Microsoft.EntityFrameworkCore;
using Data.SqlModels;
using Car_Rental_Marketplace.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CarMarketplaceContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddHttpClient("Requests", options => {options.Timeout = TimeSpan.FromSeconds(20);});
builder.Services.AddControllers();
builder.Services.AddScoped<ApiService, ApiService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();


app.Run();
