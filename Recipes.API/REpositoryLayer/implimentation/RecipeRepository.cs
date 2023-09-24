using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace RepositoryLayer.implimentation
{
    public class RecipeRepository<T> : IRecipeRepository<T> where T : recipe
    {
        private readonly RecipeDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemoryCache _cache;
        private readonly DbSet<T> entity;
        private readonly string cacheKey="Recipes";
        private  string ViewedRecipeId="";
        public RecipeRepository(RecipeDbContext dbContext, UserManager<IdentityUser> userManager, IMemoryCache cache)
        {
            _dbContext = dbContext;
            entity = _dbContext.Set<T>();
            _userManager = userManager;
            this._cache = cache;
        }


        //add recipe
        public async Task< string> AddRecipe(T recipe)
        {
            try
            {
               
                entity.Add(recipe);
                _dbContext.SaveChanges();
                return "Added Succesfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        //edit recipe
        public string EditeRecipe(int id, T recipe)
        {
            try
            {
                var recipeValue = entity.Find(id);
                if (recipeValue != null)
                {
                    recipeValue.Name = recipe.Name;
                    recipeValue.Steps = recipe.Steps;
                    recipeValue.Ingredients = recipe.Ingredients;
                    recipeValue.Image = recipe.Image;
                    _dbContext.Update(recipeValue);
                    _dbContext.SaveChanges();
                    return "updated Seccesfully";
                }
                else
                {
                    return " recipe not found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //search by name
        public IEnumerable<T> Filter(string NameOrIngreadiant)
        {
            NameOrIngreadiant = string.IsNullOrEmpty(NameOrIngreadiant) ? "" : NameOrIngreadiant.ToLower();
            List<T> filters = new List<T>();
            var recipes = from R in entity
                           where NameOrIngreadiant == "" ||
                           R.Name.ToLower().Contains(NameOrIngreadiant)
                           select new recipe
                           {
                               Id = R.Id,
                               Name = R.Name,
                               Ingredients = R.Ingredients,
                               Steps = R.Steps,
                               Image = R.Image,
                           }

                           ;
            return (IEnumerable<T>)recipes.ToList();
        }

        //search by ingredient
        public IEnumerable<T> FilterIngredients(string ingredients)
        {
            ingredients = string.IsNullOrEmpty(ingredients) ? "" : ingredients.ToLower();
            var Lista = ingredients.Split(",");
            List<T> filters = new List<T>();
            var recipes = from R in entity select R;
            foreach (var Ingr in Lista)
            {
                recipes = recipes.Where(X => X.Ingredients.ToLower().Contains(Ingr.ToLower()));
            }

            return recipes.ToList();
        }

        //image
        public string GetImageUrl(string imgName)
        {
            return "https://localhost:7206/Resourcess/images" + imgName;
        }


        //get single recipe
        public T GetRecipe(int id)
        {
            var stopWathc = new Stopwatch();
            stopWathc.Start();
            this.ViewedRecipeId= id.ToString();
            if (_cache.TryGetValue(ViewedRecipeId, out T recipe))
            {
                return recipe;
            }
            else
            {
                recipe =entity.FirstOrDefault(x => x.Id == id);
                _cache.Set(ViewedRecipeId, recipe);
                var cachEnterOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                stopWathc.Stop();
                return recipe;
            }
            
        }

        //gel all recipes
        public IEnumerable<T> GetRecipes()

        {
            var stopWathc = new Stopwatch();
            stopWathc.Start();
            if(_cache.TryGetValue(cacheKey,out IEnumerable<T> recipes))
            {
                return recipes.AsEnumerable();
            }
            else
            {
                recipes=entity.ToList();
                 _cache.Set(cacheKey,recipes);
                var cachEnterOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
            }
            stopWathc.Stop();
            return recipes.AsEnumerable();
        }


        //delet recipe
        public string RemoveRecipe(int id)
        {
            try
            {
                var DeletedRecipe = entity.Find(id);
                _dbContext.Remove(DeletedRecipe);
                _dbContext.SaveChanges(true);
                return "removed successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
       
    }
}
