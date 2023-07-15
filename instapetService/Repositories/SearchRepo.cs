using instapetService.Interfaces;
using instapetService.ServiceModel;
using instapetService.Util;
using Microsoft.EntityFrameworkCore;

namespace instapetService.Repositories
{

    public class SearchRepo : ISearchRepo
    {

        public readonly InstaPetContext _db;

        public SearchRepo(InstaPetContext db)
        {
            _db = db;
        }

        public async Task<List<SearchResult>> SearchUser(string input,int userId)
        {
            var searchResults = await (from user in _db.User
                                       join follow in _db.Follow
                                            on user.Id equals follow.UserId into followGroup
                                       from fg in followGroup.DefaultIfEmpty()
                                       where user.Username.Contains(input) && user.Id != userId
                                       select new SearchResult
                                       {
                                           Username = user.Username,
                                           UserId = user.Id,
                                           Followed = fg != null && fg.FollowerId == userId
                                       })
                                         .ToListAsync();

            return searchResults;
        }

        public async Task<SearchResult> SearchUser(int userId)
        {
            return await _db.User.Where(x => x.Id == userId).Select(x => new SearchResult()
            {
                UserId = x.Id,
                Username = x.Username,
            }).FirstOrDefaultAsync() ?? new SearchResult();
        }

        public async Task<List<SearchResult>> SearchUserMultiple(List<int> userId)
        {
            var searchResults = await _db.User.Where(x => userId.Contains(x.Id)).Select(x => new SearchResult()
            {
                UserId = x.Id,
                Username = x.Username,

            }).ToListAsync();

            return searchResults;
        }
    }
}
