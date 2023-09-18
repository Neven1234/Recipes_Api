using DomainLayer.Models;
using iTextSharp.tool.xml.html;
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
    public class IngredientsService : IIngredients
    {
        private readonly IIngredientRepository<IngredientsList> _repository;

        public IngredientsService(IIngredientRepository<IngredientsList> repository)
        {
            this._repository = repository;
        }
        public string AddIngredients(IngredientsList ingredients)
        {
            return this._repository.AddIngredients(ingredients);        }

        public IEnumerable<IngredientsList> GetIngredients()
        {
           return this._repository.GetIngredients();
        }
    }
}
