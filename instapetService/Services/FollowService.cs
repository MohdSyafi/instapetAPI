using instapetService.Interfaces;
using instapetService.Repositories;

namespace instapetService.Services
{

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
