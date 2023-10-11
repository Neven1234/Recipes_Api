using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingList _ishoppingList;

        public ShoppingListController(IShoppingList shoppingList)
        {
            this._ishoppingList = shoppingList;
        }

        //Add To The List
        [HttpPost("{UserId}")]
        public async Task<IActionResult> Add(ShoppingList shoppingList,string UserId)
        {
            var res= await _ishoppingList.Add(shoppingList,UserId);
            return Ok(res);
        }

        //Get User Shopping List
        [HttpGet("{UserId}")]
        public IActionResult GetAll(string UserId)
        {
            var res=this._ishoppingList.GetAll(UserId);
            return Ok(res);
        }

        //Get one item
        [HttpGet("getOne/{Id:int}")]
        public IActionResult Get(int Id)
        {
            var res =this._ishoppingList.get(Id);
            return Ok(res);
        }

        //Edite
        [HttpPut("{Id:int}")]
        public IActionResult Edite( int Id, ShoppingList shoppingList )
        {
            var res = this._ishoppingList.Edite(Id,shoppingList);
            return Ok(res);
        }

        //Cleare
        [HttpDelete("{UserId}")]
        public IActionResult Delete(string UserId)
        {
            var res=this._ishoppingList.Cleare(UserId);
            return Ok(res);
        }

    }
}
