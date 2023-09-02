using DomainLayer.Models;
using RepositoryLayer;
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

        public RecipeService(RecipeDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //add new recipy
        public string AddRecipe(recipe recipe)
        {
            try
            {
                this._dbContext.recipes.Add(recipe);
                this._dbContext.SaveChanges();
                return "Added Succesfully";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        //edite a recipe
        public string EditeRecipe(int id, recipe recipe)
        {
            try
            {
                var recipeValue = this._dbContext.recipes.Find(id);
                if(recipeValue != null)
                {
                    recipeValue.Name = recipe.Name;
                    recipeValue.Steps = recipe.Steps;
                    recipeValue.Ingredients = recipe.Ingredients;
                    this._dbContext.SaveChanges();
                    return "updated Seccesfully";
                }
                else
                {
                    return " recipe not found";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        //get a single recipe
        public recipe GetRecipe(int id)
        {
            return this._dbContext.recipes.Find(id);
        }

        //get all recipes
        public List<recipe> GetRecipes()
        {
            return this._dbContext.recipes.ToList();
        }

        //remove a recipy
        public string RemoveRecipe(int id)
        {
            try
            {
                var DeletedRecipe = this._dbContext.recipes.Find(id);
                this._dbContext.Remove(DeletedRecipe);
                this._dbContext.SaveChanges(true);
                return "removed successfully";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
          

        }
    }
}
