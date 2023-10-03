using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface ICategory
    {
        IEnumerable<CategoryList> GetCategories();
        Task<string> AddCategory(CategoryList category);
    }
}
