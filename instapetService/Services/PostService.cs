using instapetService.Models;
using instapetService.Repositories;
using instapetService.ServiceModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Services
{

    public interface IPostService
    {
        Task<List<PostAndImages>> GetPosts(List<int> FollowedUsersList);

        Task<bool> AddPost(PostAndImages post);
    }
    public class ImageDirConfig
    {
        public string ImageDir { get; set; }
    }

    public class PostService : IPostService
    {
        private IPostRepo _postRepo;
        private IImageRepo _imageRepo;
        private readonly ImageDirConfig _imageDirConfig;

        public PostService(IPostRepo postRepo,IImageRepo imageRepo,ImageDirConfig config)
        {
            _postRepo = postRepo;
            _imageRepo = imageRepo; 
            _imageDirConfig = config;
        }

        public async Task<bool> AddPost(PostAndImages post)
        {
            try
            {
                bool result = true;

                var postId = await _postRepo.AddPost(post);

                if (post.formFiles == null)
                    return result;

                if(!post.formFiles.Any())
                    return result;

                var images = post.formFiles.Select(x => new Image { PostId = postId, Name = x.FileName }).ToList();
                result = await _imageRepo.AddImages(images);

                List<Image> savedImages = await _imageRepo.GetImages(postId);

                var imageDir = _imageDirConfig.ImageDir + $"post-{postId}/";

                if (!Directory.Exists(imageDir))
                {
                    Directory.CreateDirectory(imageDir);
                }

                foreach (var image in savedImages)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(),imageDir, image.Name.ToString());

                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        var fileForm = post.formFiles.Select(x => x).Where(x => x.FileName == image.Name).FirstOrDefault();

                        if (fileForm != null)
                            await fileForm.CopyToAsync(stream);
                    }
                }

                return result;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<PostAndImages>> GetPosts(List<int> FollowedUsersList)
        {
            var posts = await _postRepo.GetPosts(FollowedUsersList);

            List<PostAndImages> postAndImages = new List<PostAndImages>();

            foreach(var post in posts)
            {
                var postAndImage = new PostAndImages()
                {
                    PostId = post.PostId,
                    UserId = post.UserId,
                    Description = post.Description,
                    Likes = post.Likes

                };

                postAndImage.images = await _imageRepo.GetImages(post.PostId);

                postAndImages.Add(postAndImage);    
           
            }
            return postAndImages;

        }
    }
}
