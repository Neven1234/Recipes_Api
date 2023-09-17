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

        public IngredientsController(IIngredients ingredients)
        {
            this._ingredients = ingredients;
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

    }
}
