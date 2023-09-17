using DomainLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RepositoryLayer;
using RepositoryLayer.implimentation;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using ServiceLayer.Services.Implimentations;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipeDbContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));

builder.Services.AddScoped<IRecipe, RecipeService>();
builder.Services.AddScoped<IIngredients, IngredientsService>();

builder.Services.AddScoped<IRepository<recipe>,RepositoryImp<recipe>>();
builder.Services.AddScoped<IRepUser<User>,RepUserImpl<User>>();
builder.Services.AddScoped<Iuser, userServices>();


builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

//identy

builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddEntityFrameworkStores<RecipeDbContext>().
    AddDefaultTokenProviders();
//authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resourcess")),
    RequestPath = new PathString("/Resourcess")
});

app.UseCors(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
