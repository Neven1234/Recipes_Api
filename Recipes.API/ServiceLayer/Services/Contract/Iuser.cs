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

        //Get user

        Task< User> GetUser( string userId);
        
        //get userId
        Task< string> UserId(string user);

        //Update user
        Task< string> UpdateUser(User user, string username);
        Task< string> ChangePassword( ChangePassword changePassword,string user);

        //reset password
        public Task< string> ForgertPassword(string userName);
        public Task<string> resetPassword(string token, string email);
        public Task<string> ResetPassword(ResetPassword resetPassword,string username);

        //confirmation
        public Task<string> Confirmation(string token, string email);


    }
}
