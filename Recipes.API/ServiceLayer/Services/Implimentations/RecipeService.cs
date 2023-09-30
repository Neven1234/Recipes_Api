using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ServiceLayer.Services.Implimentations
{
    public class RecipeService : IRecipe
    {
        private readonly RecipeDbContext _dbContext;
        private readonly IRepository<recipe> _repository;
        private readonly UserManager<IdentityUser> _userManager;
        public string imgname;
        private readonly IMemoryCache _cache;
        private readonly string cacheKey = "Recipes";
        private string ViewedRecipeId = "";

        public RecipeService(RecipeDbContext dbContext , IRepository<recipe> repository , UserManager<IdentityUser> userManager, IMemoryCache cache)
        {
            this._dbContext = dbContext;
            this._repository = repository;
            this._userManager = userManager;
            this._cache = cache;
        }

        //add new recipy
        public async Task< string> AddRecipe(recipe recipe, string username)
        {
           
            _cache.Remove(cacheKey);
            var user = await _userManager.FindByNameAsync(username);
            recipe.UserName = user.Id;
            return await _repository.Add(recipe );
        }

        //edite
        public string EditeRecipe(int id, recipe recipe)
        {
            _cache.Remove(cacheKey);
            return this._repository.Edite(id, recipe);
        }

        //filter
        public IEnumerable<recipe> Filter(string NameOrIngreadiant)
        {
            NameOrIngreadiant = string.IsNullOrEmpty(NameOrIngreadiant) ? "" : NameOrIngreadiant.ToLower();
            List<recipe> filters = new List<recipe>();
            var recipes = from R in _dbContext.recipes
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
            return (IEnumerable<recipe>)recipes.ToList();
        }

        public IEnumerable<recipe> FilterIngredients(string ingredients)
        {
            ingredients = string.IsNullOrEmpty(ingredients) ? "" : ingredients.ToLower();
            var Lista = ingredients.Split(",");
            List<recipe> filters = new List<recipe>();
            var recipes = from R in _dbContext.recipes select R;
            foreach (var Ingr in Lista)
            {
                recipes = recipes.Where(X => X.Ingredients.ToLower().Contains(Ingr.ToLower()));
            }

            return recipes.ToList();
        }

        
        //get single recipe
        public recipe GetRecipe(int id)
        {
            var stopWathc = new Stopwatch();
            stopWathc.Start();
            this.ViewedRecipeId = id.ToString();
            if (_cache.TryGetValue(ViewedRecipeId, out recipe recipe))
            {
                return recipe;
            }
            else
            {
                recipe = this._repository.GetById(id);
                _cache.Set(ViewedRecipeId, recipe);
                var cachEnterOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                stopWathc.Stop();
                return recipe;
            }
            
        }

        //get all recipes
        public IEnumerable<recipe> GetRecipes()
        {
            var stopWathc = new Stopwatch();
            stopWathc.Start();
            if (_cache.TryGetValue(cacheKey, out IEnumerable<recipe> recipes))
            {
                return recipes.AsEnumerable();
            }
            else
            {
                recipes= this._repository.GetAll().ToList();
                _cache.Set(cacheKey, recipes);
                var cachEnterOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
            }
            stopWathc.Stop();
            return recipes.AsEnumerable();

        }

        //delete recipe
        public string RemoveRecipe(int id)
        {
            _cache.Remove(cacheKey);
            return this._repository.Remove(id);
        }

        //get user recipes
        public async Task<IEnumerable<recipe>> RecipesOfUser(string username)
        {
            var user= await _userManager.FindByNameAsync(username);
            var recipes = from R in _dbContext.recipes
                          where username == "" ||
                          R.UserName == user.Id
                          select new recipe
                          {
                              Id = R.Id,
                              Name = R.Name,
                              Ingredients = R.Ingredients,
                              Steps = R.Steps,
                              Image = R.Image,
                          }

                          ;
            return (IEnumerable<recipe>)recipes.ToList();
        }




        //testing



    }
}
