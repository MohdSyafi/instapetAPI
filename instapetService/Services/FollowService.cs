using instapetService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Services
{
    public interface IFollowService
    {
        Task<bool> FollowUser(int userId, int userIdToFollow);
        Task<bool> UnFollowUser(int userId, int userIdToUnFollow);
    }
    public class FollowService : IFollowService
    {
        public readonly IFollowRepo _followRepo;

        public FollowService(IFollowRepo followRepo)
        {
            _followRepo = followRepo;
        }

        public async Task<bool> FollowUser(int userId, int userIdToFollow)
        {
           return await _followRepo.FollowUser(userId, userIdToFollow);
        }

        public async Task<bool> UnFollowUser(int userId, int userIdToUnFollow)
        {
            return await _followRepo.UnFollowUser(userId, userIdToUnFollow);
        }
    }
}
