using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface IFollow
    {
        Task<string> AddFollow(Follow follow,string UserId);
        IEnumerable<Follow> GetFollows(string UserId);
    }
}
