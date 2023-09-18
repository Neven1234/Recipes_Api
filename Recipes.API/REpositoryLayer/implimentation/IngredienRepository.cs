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
    public class IngredienRepository<T> : IIngredientRepository<T> where T : IngredientsList
    {
        private readonly RecipeDbContext _dbContext;
        private readonly DbSet<T> entity;

        public IngredienRepository(RecipeDbContext dbContext)
        {
            this._dbContext = dbContext;
            this.entity =this._dbContext.Set<T>();
        }
        public string AddIngredients(T ingredients)
        {
            try
            {
                this.entity.Add(ingredients);
                this._dbContext.SaveChanges();
                return "Added Succesfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IEnumerable<T> GetIngredients()
        {
            return entity.AsEnumerable();
        }
    }
}
