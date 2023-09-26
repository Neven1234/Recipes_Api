using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface IIngredients
    {
        //get all ingerdiance
        IEnumerable<IngredientsList> GetIngredients();
       Task< string> AddIngredients(IngredientsList ingredients);
    }
}
