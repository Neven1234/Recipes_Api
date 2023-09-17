using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IRepository<T> where T : recipe
    {
        //Get all recipes
        IEnumerable<T> GetRecipes();

        //get singl recipe
        T GetRecipe(int id);

        // add a recipe
        string AddRecipe(T recipe);

        //get image url
        string GetImageUrl(string imgName);
        //edite a recipe
        string EditeRecipe(int id, T recipe);

        //delet a recipe
        string RemoveRecipe(int id);

        //testing 

        //search by name
        IEnumerable<T> Filter(string NameOrIngreadiant);

        //search by ingredients
        IEnumerable<T> FilterIngredients(string ingredients);
    }
}
