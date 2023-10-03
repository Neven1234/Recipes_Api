using DomainLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using RepositoryLayer;
using RepositoryLayer.implimentation;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using ServiceLayer.Services.Implimentations;
using System.Net.NetworkInformation;
using System.Web.Helpers;
using UserManger.Models;
using UserManger.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RecipeDbContext>(option=>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ConnString"));
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

builder.Services.AddScoped<IRecipe, RecipeService>();
builder.Services.AddScoped<IIngredients, IngredientsService>();
builder.Services.AddScoped(typeof(IRepository<>),typeof(RepositoryImplementation<>));
builder.Services.AddScoped<Iuser, userServices>();
builder.Services.AddScoped<IRateAndReview, RateAndReviewService>();
builder.Services.AddScoped<ICategory,CategoryServicecs>();
builder.Services.AddScoped<IFavorite, FavoriteService>();


builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(option =>
    option.TokenLifespan = TimeSpan.FromHours(10));
//configuration email
var emailConfig = configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfigration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmail, EmailService>();

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
//cahe
builder.Services.AddMemoryCache();


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
