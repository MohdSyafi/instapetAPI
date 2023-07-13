using instapetService.ServiceModel;

namespace instapetService.Interfaces
{
    public interface ISearchRepo
    {
        Task<List<SearchResult>> SearchUser(string input, int userId);
        Task<List<SearchResult>> SearchUserMultiple(List<int> userId);
    }
}
