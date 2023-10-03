using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.implimentation
{
    public class RepositoryImplementation<T> : IRepository<T> where T : class
    {
        private readonly RecipeDbContext _dbContext;
        private readonly DbSet<T> entity;

        public RepositoryImplementation(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
            entity =  _dbContext.Set<T>();
        }
        public async Task<string> Add(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                this._dbContext.SaveChanges();
                return "Added Succesfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Edite(int id, T entityy)
        {
            try
            {
                var entityValue = this.entity.Find(id);
                if (entityValue != null)
                {
                    entityValue = entityy;
                    _dbContext.Update(entityValue);
                    _dbContext.SaveChanges();
                    return "updated Seccesfully";
                }
                else
                {
                    return " not found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            var data = filter == null ? entity.ToList() : entity.Where(filter).ToList();
            return data;
        }

        public T GetById(int id)
        {
            var result = entity.Find(id);
            return result;
        }

        public string Remove(int id)
        {
            try
            {
                var DeletedEntity = entity.Find(id);
                _dbContext.Remove(DeletedEntity);
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
