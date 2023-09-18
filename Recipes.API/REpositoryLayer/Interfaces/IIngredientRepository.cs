using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IIngredientRepository<T> where T:IngredientsList
    {
        //get all ingerdiance
        IEnumerable<T> GetIngredients();
        string AddIngredients(T ingredients);
    }
}
