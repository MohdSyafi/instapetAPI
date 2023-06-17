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

    public interface ISearchRepo
    {
        Task<List<User>> SearchUser(string input);
    }
    public class SearchRepo : ISearchRepo
    {

        public readonly InstaPetContext _db;

        public SearchRepo(InstaPetContext db)
        {
            _db = db;
        }

        public async Task<List<User>> SearchUser(string input)
        {
            return await _db.User.Where(x=>x.Username.Contains(input)).ToListAsync();
        }
    }
}
