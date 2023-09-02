using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ServiceLayer.Services.Contract;
using ServiceLayer.Services.Implimentations;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipeDbContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));

builder.Services.AddScoped<IRecipe, RecipeService>();
builder.Services.AddScoped<IIngredients, IngredientsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
