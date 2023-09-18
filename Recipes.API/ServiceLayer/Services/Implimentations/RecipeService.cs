using DomainLayer.Models;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ServiceLayer.Services.Implimentations
{
    public class RecipeService : IRecipe
    {
        private readonly RecipeDbContext _dbContext;
        private readonly IRecipeRepository<recipe> _repository;
        public string imgname;
        
        public RecipeService(RecipeDbContext dbContext , IRecipeRepository<recipe> repository )
        {
            this._dbContext = dbContext;
            this._repository = repository;
        }

        //add new recipy
        public string AddRecipe(recipe recipe)
        {
           return this._repository.AddRecipe(recipe);
        }

        public string EditeRecipe(int id, recipe recipe)
        {
          return this._repository.EditeRecipe(id, recipe);
        }

        public IEnumerable<recipe> Filter(string NameOrIngreadiant)
        {
            return this._repository.Filter(NameOrIngreadiant);
        }

        public IEnumerable<recipe> FilterIngredients(string ingredients)
        {
            return this._repository.FilterIngredients(ingredients);
        }

        public string GetImageUrl(string imgName)
        {
           return this._repository.GetImageUrl(imgName);
        }

        public recipe GetRecipe(int id)
        {
           return this._repository.GetRecipe(id);
        }

        public IEnumerable<recipe> GetRecipes()
        {
            return this._repository.GetRecipes();
        }

        public string RemoveRecipe(int id)
        {
            return this._repository.RemoveRecipe(id);
        }


       

        //testing



    }
}
