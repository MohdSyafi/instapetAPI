using Amazon.S3;
using Amazon.S3.Model;
using instapetService.Configs;
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


    public class PostService : IPostService
    {
        private IPostRepo _postRepo;
        private IImageRepo _imageRepo;
        private readonly AwsConfig _awsConfig;

        public PostService(IPostRepo postRepo,IImageRepo imageRepo, AwsConfig config)
        {
            _postRepo = postRepo;
            _imageRepo = imageRepo;
            _awsConfig = config;
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

                var imageDir = _awsConfig.S3ImageDir + $"post-{postId}/";

                var client = new AmazonS3Client(_awsConfig.accessKey, _awsConfig.accessSecret, Amazon.RegionEndpoint.APSoutheast1);

                foreach (var image in savedImages)
                {
                    var fileForm = post.formFiles.Select(x => x).Where(x => x.FileName == image.Name).FirstOrDefault();

                    if (fileForm == null)
                        continue;

                    byte[] fileBytes = new byte[fileForm.Length];
                    fileForm.OpenReadStream().Read(fileBytes,0,Int32.Parse(fileForm.Length.ToString()));

                    var fileName = fileForm.FileName;
                    var bucketPath = _awsConfig.S3Bucket;
                    PutObjectResponse response = null;

                    using(var stream = new MemoryStream(fileBytes))
                    {
                        var request = new PutObjectRequest
                        {
                            BucketName = bucketPath,
                            Key = fileName,
                            InputStream = stream,
                            ContentType = fileForm.ContentType
                        };

                        response = await client.PutObjectAsync(request);
                    }

                    if(response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;

                    }
                    else
                        return false;

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
