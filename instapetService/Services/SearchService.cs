using instapetService.Models;
using instapetService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Services
{
    public interface ISearchService
    {
        Task<List<User>> SearchUser(string input);
    }

    public class SearchService : ISearchService
    {
        public readonly ISearchRepo _searchRepo;

       public SearchService(ISearchRepo searchRepo)
       {
            _searchRepo = searchRepo;
       }

        public async Task<List<User>> SearchUser(string input)
        {
            return await _searchRepo.SearchUser(input);
        }
    }
}
