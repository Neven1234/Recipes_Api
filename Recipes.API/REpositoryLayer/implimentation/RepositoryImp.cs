using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.implimentation
{
    public class RepositoryImp<T> : IRepository<T> where T : recipe
    {
        private readonly RecipeDbContext _dbContext;
        private readonly DbSet<T> entity;

        public RepositoryImp(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
            entity = _dbContext.Set<T>();
        }


        //add recipe
        public string AddRecipe(T recipe)
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
            return entity.FirstOrDefault(x => x.Id == id);
        }

        //gel all recipes
        public IEnumerable<T> GetRecipes()
        {
            return entity.AsEnumerable();
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
