using DomainLayer.Models;
using Microsoft.Extensions.Caching.Memory;
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
    public class CategoryServicecs : ICategory
    {
        private readonly IRepository<CategoryList> _repository;
        private readonly IMemoryCache _cache;
        private readonly string cacheKey = "Category";
        public CategoryServicecs(IRepository<CategoryList> repository, IMemoryCache cache)
        {
            this._repository = repository;
            this._cache = cache;    
        }
        public Task<string> AddCategory(CategoryList category)
        {
            _cache.Remove(cacheKey);
            return  _repository.Add(category);
        }

        public IEnumerable<CategoryList> GetCategories()
        {
            var stopWathc = new Stopwatch();
            stopWathc.Start();
            if (_cache.TryGetValue(cacheKey, out IEnumerable<CategoryList> categories))
            {
                return categories.AsEnumerable();
            }
            else
            {
                categories = this._repository.GetAll().ToList();
                _cache.Set(cacheKey, categories);
                var cachEnterOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
            }
            stopWathc.Stop();
            return categories;
        }
    }
}
