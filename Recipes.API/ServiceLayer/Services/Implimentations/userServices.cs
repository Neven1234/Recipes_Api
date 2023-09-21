using DomainLayer.Models;
using RepositoryLayer.Interfaces;
using ServiceLayer.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implimentations
{
    public class userServices : Iuser
    {
        private readonly IUserRepository<User> _repUser;

        public userServices(IUserRepository<User> repUser)
        {
            this._repUser = repUser;
        }
        public Task< string> LogIn(User user)
        {
            return this._repUser.LogIn(user);
        }

        public Task<string> Regist(User user)
        {
            return this._repUser.Regist(user);
        }
    }
}
