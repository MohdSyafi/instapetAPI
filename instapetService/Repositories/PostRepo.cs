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

    public interface IPostRepo
    {
        Task<List<Post>> GetPosts(int UserId);

        Task<List<Post>> GetPosts(List<int> FollowedUsersList);

        Task<int> AddPost(Post post);
    }

    public class PostRepo : IPostRepo
    {
        public readonly InstaPetContext _db;

        public PostRepo(InstaPetContext instaPetContext) {
            _db = instaPetContext;        
        }
        public async Task<int> AddPost(Post post)
        {
            try
            {
                await _db.Post.AddAsync(post);
                await _db.SaveChangesAsync();
                return post.PostId;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<List<Post>> GetPosts(int UserId)
        {
            var posts = await _db.Post.Where(x => x.UserId == UserId).ToListAsync();

            return posts;
        }

        public async Task<List<Post>> GetPosts(List<int> FollowedUsersList)
        {
            var posts = await _db.Post.Where(x => FollowedUsersList.Contains(x.UserId)).ToListAsync();

            return posts;
        }
    }
}
