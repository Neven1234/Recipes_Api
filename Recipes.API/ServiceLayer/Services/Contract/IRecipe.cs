using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface IRecipe
    {
        //Get all recipes
        IEnumerable<recipe> GetRecipes();

        //get singl recipe
        recipe GetRecipe(int id);

        // add a recipe
        string AddRecipe(recipe recipe);

        //get image url
        string GetImageUrl(string imgName);
        //edite a recipe
        string EditeRecipe(int id, recipe recipe);

        //delet a recipe
        string RemoveRecipe(int id);

        //testing 

        //search by name
        IEnumerable<recipe> Filter(string NameOrIngreadiant);

        //search by ingredients
        IEnumerable<recipe> FilterIngredients(string ingredients);
    }
}
