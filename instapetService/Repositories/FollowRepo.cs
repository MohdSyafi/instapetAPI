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
    public interface IFollowRepo
    {
        Task<List<int>> GetFollowers(int userId);
        Task<List<int>> GetFollowing(int userId);
        Task<bool> FollowUser(int userId, int userIdToFollow);
        Task<bool> UnFollowUser(int userId, int userIdToUnFollow);
    }
    public class FollowRepo : IFollowRepo
    {
        public readonly InstaPetContext _db;

        public FollowRepo(InstaPetContext db)
        {
            _db = db;
        }

        public async Task<bool> FollowUser(int userId, int userIdToFollow)
        {
            try
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
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UnFollowUser(int userId, int userIdToUnFollow)
        {
            try
            {
                var currentFollow = _db.Follow.Where(x=>x.UserId == userIdToUnFollow && x.FollowerId == userId).FirstOrDefault();

                if(currentFollow != null)
                {
                   _db.Follow.Remove(currentFollow);
                   await _db.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
