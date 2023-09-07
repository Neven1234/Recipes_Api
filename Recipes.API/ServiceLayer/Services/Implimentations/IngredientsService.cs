using DomainLayer.Models;
using iTextSharp.tool.xml.html;
using RepositoryLayer;
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
        private readonly RecipeDbContext _dbContext;

        public IngredientsService(RecipeDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public string AddIngredients(IngredientsList ingredients)
        {
            try
            {
                this._dbContext.ingredients.Add(ingredients);
                this._dbContext.SaveChanges();
                return "Added Succesfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<IngredientsList> GetIngredients()
        {
            return this._dbContext.ingredients.ToList();
        }
    }
}
