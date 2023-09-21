using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository<T> where T : User
    {
        //Regist
        Task<string> Regist(T user);
        //Log in
       Task< string> LogIn(T user);

       
    }
}
