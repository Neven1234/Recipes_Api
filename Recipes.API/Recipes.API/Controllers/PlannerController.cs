using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Contract;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannerController : ControllerBase
    {
        private readonly IPlanner _iplanner;

        public PlannerController(IPlanner planner)
        {
            this._iplanner = planner;
        }
        [HttpGet("{userId}")]
        public IActionResult GetAll(string userId)
        {
            var res=this._iplanner.GetAll(userId);
            return Ok(res);
        }
        [HttpPost("{userID}")]
        public async Task< IActionResult> Add(Planner planner,string userID)
        {
            var res =await _iplanner.Add (planner,userID);
            return Ok(res);
        }

        [HttpGet("{Id:int}")]
        public IActionResult GetById(int Id)
        {
            var res = this._iplanner.GetOne(Id);
            return Ok(res);
        }
        [HttpPut("{Id:int}")]
        public IActionResult Edit(Planner planner,int Id)
        {
            var res = this._iplanner.Edit(Id, planner);
            return Ok(res);
        }
    }
}
