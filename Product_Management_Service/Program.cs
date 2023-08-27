using Microsoft.EntityFrameworkCore;
using Product_Management_Service;
using Product_Management_Service.Context;
using Product_Management_Service.Repository;
using Product_Management_Service.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// create connection string
builder.Services.AddDbContext<vsm_db_productContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("dbCon"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// create depedency injection
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddAutoMapper(typeof(MappingConfig));

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
