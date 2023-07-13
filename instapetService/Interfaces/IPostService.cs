using instapetService.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Interfaces
{
    public interface IPostService
    {
        Task<List<PostAndImages>> GetPosts(int userId);

        Task<bool> AddPost(PostAndImages post);
    }
}
