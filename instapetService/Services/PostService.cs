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
        Task<List<PostAndImages>> GetPosts(int userId);

        Task<bool> AddPost(PostAndImages post);
    }


    public class PostService : IPostService
    {
        private IPostRepo _postRepo;
        private IFollowRepo _followRepo;
        private IImageRepo _imageRepo;
        private ISearchRepo _searchRepo;
        private readonly AwsConfig _awsConfig;

        public PostService(IPostRepo postRepo,IImageRepo imageRepo, AwsConfig config, IFollowRepo followRepo, ISearchRepo searchRepo)
        {
            _postRepo = postRepo;
            _imageRepo = imageRepo;
            _awsConfig = config;
            _followRepo = followRepo;
            _searchRepo = searchRepo;
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
                            Key = imageDir + fileName,
                            InputStream = stream,
                            ContentType = fileForm.ContentType
                        };

                        response = await client.PutObjectAsync(request);
                    }

                    if(response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                        result = false;

                }


                return result;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<PostAndImages>> GetPosts(int userId)
        {
            var FollowedUsersList = await _followRepo.GetFollowing(userId);
            var posts = await _postRepo.GetPosts(FollowedUsersList);
            var users = await _searchRepo.SearchUserMultiple(FollowedUsersList);
            var userDictionary = users.ToDictionary(u => u.UserId, u => u.Username);

            List<PostAndImages> postAndImages = new List<PostAndImages>();

            foreach(var post in posts)
            {
                var postAndImage = new PostAndImages()
                {
                    PostId = post.PostId,
                    UserId = post.UserId,
                    Description = post.Description,
                    Likes = post.Likes,
                    userName = userDictionary[post.UserId]

                };

                postAndImage.images = await _imageRepo.GetImages(post.PostId);


                foreach(var image in postAndImage.images)
                {
                    string bucketName = _awsConfig.S3Bucket;
                    string objectKey = _awsConfig.S3ImageDir + $"post-{post.PostId}/" + image.Name;

                    const double timeoutDuration = 12;

                    IAmazonS3 s3Client = new AmazonS3Client(_awsConfig.accessKey, _awsConfig.accessSecret, Amazon.RegionEndpoint.APSoutheast1);

                    string urlString = GeneratePresignedURL(s3Client, bucketName, objectKey, timeoutDuration);

                    image.Location = urlString;
                } 

                postAndImages.Add(postAndImage);    
           
            }
            return postAndImages;

        }

        // <summary>
        // Generate a presigned URL that can be used to access the file named
        // in the objectKey parameter for the amount of time specified in the
        // duration parameter.
        // </summary>
        // <param name="client">An initialized S3 client object used to call
        // the GetPresignedUrl method.</param>
        // <param name="bucketName">The name of the S3 bucket containing the
        // object for which to create the presigned URL.</param>
        // <param name="objectKey">The name of the object to access with the
        // presigned URL.</param>
        // <param name="duration">The length of time for which the presigned
        // URL will be valid.</param>
        // <returns>A string representing the generated presigned URL.</returns>
        public static string GeneratePresignedURL(IAmazonS3 client, string bucketName, string objectKey, double duration)
        {
            string urlString = string.Empty;
            try
            {
                var request = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = objectKey,
                    Expires = DateTime.UtcNow.AddHours(duration),
                };
                urlString = client.GetPreSignedURL(request);
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error:'{ex.Message}'");
            }

            return urlString;
        }
    }
}
