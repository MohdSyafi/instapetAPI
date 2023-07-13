using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Interfaces
{
    public interface IFollowRepo
    {
        Task<List<int>> GetFollowers(int userId);
        Task<List<int>> GetFollowing(int userId);
        Task<bool> FollowUser(int userId, int userIdToFollow);
        Task<bool> UnFollowUser(int userId, int userIdToUnFollow);
    }
}
