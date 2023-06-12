using instapetService.Models;
using instapetService.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Repositories
{
  
    public interface ILoginRepo
    {

        Task<User> GetUser(string searchInput);

        Task<bool> AddUser(User user);
    }
    public class LoginRepo : ILoginRepo
    {
        private readonly InstaPetContext _db;
        public LoginRepo(InstaPetContext instaPetContext) { 
            
            this._db = instaPetContext;
        }
        public async Task<User> GetUser(string searchInput)
        {
           var query = _db.User.Select(x=>x);

            if (!string.IsNullOrEmpty(searchInput))
            {
                query = query.Where(x => x.Username == searchInput || x.Email == searchInput);

                var res = await query.FirstOrDefaultAsync();

                return res;
            }
               
            return new User();
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {

                await _db.User.AddAsync(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
