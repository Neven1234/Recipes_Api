using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class IngredientsController : Controller
    {
        private readonly IIngredients _ingredients;
        private readonly ICategory _icategory;

        public IngredientsController(IIngredients ingredients,ICategory category)
        {
            this._ingredients = ingredients;
            this._icategory = category;
        }
        [HttpGet]
        public IActionResult GetIngredients()
        {
            var res=this._ingredients.GetIngredients();
            return Ok(res);
        }
        [HttpPost]
        public IActionResult AddIngredient(IngredientsList ingredient)
        {
            var res = this._ingredients.AddIngredients(ingredient);
            return Ok(res);

        }

        //category
        [HttpGet("Category")]
        public IActionResult GetAllCategory()
        {
            var res=this._icategory.GetCategories();
            return Ok(res);
        }

        [HttpPost("Category")]
        public IActionResult AddCategory(CategoryList category)
        {
            var res= this._icategory.AddCategory(category);
            return Ok(res);
        }
    }
}
