using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IRepUser<T> where T : User
    {
        //Regist
        Task<string> Regist(T user);
        //Log in
        string LogIn(T user);

       
    }
}
