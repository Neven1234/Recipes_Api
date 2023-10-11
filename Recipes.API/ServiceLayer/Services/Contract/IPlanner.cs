using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface IPlanner
    {
        Task<string> Add(Planner planner, string userId);
        Planner GetOne(int Id);
        IEnumerable<Planner> GetAll(string userId);

        string Edit(int id, Planner planner);
    }
}
