using DomainLayer.Models;
using iTextSharp.tool.xml.html;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ServiceLayer.Services.Implimentations
{
    public class IngredientsService : IIngredients
    {
        private readonly IRepository<IngredientsList> _repository;
        private readonly IMemoryCache _cache;
        private readonly string cacheKey = "Ingredients";
        public IngredientsService(IRepository<IngredientsList> repository,IMemoryCache cache)
        {
            this._repository = repository;
            this._cache = cache;
        }
        public async Task< string> AddIngredients(IngredientsList ingredients)
        {
            _cache.Remove(cacheKey);
            return await _repository.Add(ingredients); 
        }

        public IEnumerable<IngredientsList> GetIngredients()
        {
            var stopWathc = new Stopwatch();
            stopWathc.Start();
            if (_cache.TryGetValue(cacheKey, out IEnumerable<IngredientsList> ingredients))
            {
                return ingredients.AsEnumerable();
            }
            else
            {
                ingredients=this._repository.GetAll().ToList();
                _cache.Set(cacheKey, ingredients);
                var cachEnterOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
            }
            stopWathc.Stop();
            return ingredients;
        }
    }
}
