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

        public string imgname;
        public RecipeService(RecipeDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        //add new recipy
        public string AddRecipe(recipe recipe)
        {
           
            try
            {
                var Recipe = new recipe()
                {
                    Name = recipe.Name,
                    Ingredients = recipe.Ingredients,
                    Steps = recipe.Steps,
                    Image = recipe.Image,
                };
                this._dbContext.recipes.Add(Recipe);
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
                    recipeValue.Image = recipe.Image;
                    this._dbContext.Update(recipeValue);
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

        //search function
        public List< recipe> Filter(string NameOrIngreadiant)
        {
            NameOrIngreadiant = string.IsNullOrEmpty(NameOrIngreadiant) ? "" : NameOrIngreadiant.ToLower();
           List<recipe> filters = new List<recipe>();
            var recipes = (from R in this._dbContext.recipes
                           where NameOrIngreadiant == "" ||
                           R.Name.ToLower().Contains(NameOrIngreadiant)
                           || R.Ingredients.ToLower().Contains(NameOrIngreadiant)
                            select new recipe
                            {
                                Id = R.Id,
                                Name = R.Name,
                                Ingredients = R.Ingredients,
                                Steps   = R.Steps,
                                Image   = R.Image,
                            }

                           );
            return recipes.ToList();
                        
        }

        //get image url
        public string GetImageUrl(string imgName)
        {
            return "https://localhost:7206/Resourcess/images" + imgName;
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
        //testing
        


    }
}
