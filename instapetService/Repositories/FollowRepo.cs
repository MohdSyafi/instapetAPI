using instapetService.Interfaces;
using instapetService.Models;
using instapetService.Util;
using Microsoft.EntityFrameworkCore;

namespace instapetService.Repositories
{

    public class FollowRepo : IFollowRepo
    {
        public readonly InstaPetContext _db;

        public FollowRepo(InstaPetContext db)
        {
            _db = db;
        }

        public async Task<bool> FollowUser(int userId, int userIdToFollow)
        {
          
            var newFollow = new Follow()
            {
                UserId = userIdToFollow,
                FollowerId = userId
            };

            await _db.Follow.AddAsync(newFollow);
            await _db.SaveChangesAsync();
            return true;

        }
        public async Task<bool> UnFollowUser(int userId, int userIdToUnFollow)
        {
            
            var currentFollow = _db.Follow.Where(x=>x.UserId == userIdToUnFollow && x.FollowerId == userId).FirstOrDefault();

            if(currentFollow != null)
            {
                _db.Follow.Remove(currentFollow);
                await _db.SaveChangesAsync();
            }

            return true;

        }
        public Task<List<int>> GetFollowers(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> GetFollowing(int userId)
        {
            var followerList = await _db.Follow.Where(x => x.FollowerId == userId).Select(x => x.UserId).ToListAsync();

            return followerList;
        }


    }
}
