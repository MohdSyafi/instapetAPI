using instapetService.Models;


namespace instapetService.Interfaces
{
    public interface IPostRepo
    {
        Task<List<Post>> GetPosts(int UserId);

        Task<List<Post>> GetPosts(List<int> FollowedUsersList);

        Task<int> AddPost(Post post);
    }
}
