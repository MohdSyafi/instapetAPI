using instapetService.Interfaces;
using instapetService.Models;
using instapetService.Util;
using Microsoft.EntityFrameworkCore;

namespace instapetService.Repositories
{
  
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

                return res??new User();
            }
               
            return new User();
        }

        public async Task<bool> AddUser(User user)
        {       
            await _db.User.AddAsync(user);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
