using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Interfaces;
using RepositoryLayer;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implimentations
{
    public class PlannerService : IPlanner
    {
        private readonly RecipeDbContext _dbContext;
        private readonly IRepository<Planner> _irepository;
        private readonly UserManager<IdentityUser> _userManager;

        public PlannerService(RecipeDbContext dbContext, IRepository<Planner> repository, UserManager<IdentityUser> userManager)
        {
            this._dbContext = dbContext;
            this._irepository = repository;
            this._userManager = userManager;
        }
        public async Task<string> Add(Planner planner, string userId)
        {
            try
            {
               
                planner.UserId = userId;
                var res = await _irepository.Add(planner);
                return res;
                
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string Edit(int id, Planner planner)
        {
            var res=this._irepository.Edite(id, planner);
            return res;
        }

        public IEnumerable<Planner> GetAll(string userId)
        {
            var res= this._irepository.GetAll(x=>x.UserId==userId);
            return res;
        }

        public Planner GetOne(int Id)
        {
            var res=  _irepository.GetById(Id);
            return res;
        }
    }
}
