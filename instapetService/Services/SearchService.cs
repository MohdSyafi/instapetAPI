using instapetService.Interfaces;
using instapetService.Repositories;
using instapetService.ServiceModel;


namespace instapetService.Services
{


    public class SearchService : ISearchService
    {
        public readonly ISearchRepo _searchRepo;

       public SearchService(ISearchRepo searchRepo)
       {
            _searchRepo = searchRepo;
       }

        public async Task<List<SearchResult>> SearchUser(string input,int UserId)
        {
            return await _searchRepo.SearchUser(input,UserId);
        }
    }
}
