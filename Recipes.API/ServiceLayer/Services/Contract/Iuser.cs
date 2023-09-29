using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Contract
{
    public interface Iuser
    {
        //log in
        
       Task< string> LogIn(User user);

        //regist
        Task<string> Register(User user);

        //reset password
        public Task< string> ForgertPassword(string userName);
        
    }
}
