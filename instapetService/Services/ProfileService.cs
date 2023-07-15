using Amazon.S3;
using Amazon.S3.Model;
using instapetService.Configs;
using instapetService.Interfaces;
using instapetService.Models;
using instapetService.ServiceModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Services
{
    public class ProfileService : IProfileService
    {
        public ISearchRepo _searchRepo;
        public IImageRepo _imageRepo;
        public IPostRepo _postRepo;
        public IFollowRepo _followRepo;
        private readonly AwsConfig _awsConfig;

        public ProfileService(IOptions<AwsConfig> config, ISearchRepo searchRepo,IImageRepo imageRepo,IPostRepo postRepo, IFollowRepo followRepo) { 
            _searchRepo = searchRepo;
            _imageRepo = imageRepo;
            _postRepo = postRepo;
            _followRepo = followRepo;
            _awsConfig = config.Value;
        }
        public async Task<ProfileResponse> GetProfile(int userId)
        {
            var user = await _searchRepo.SearchUser(userId);
            var posts = await _postRepo.GetPosts(userId);
            var followers = await _followRepo.GetFollowers(userId);
            var followingList = await _followRepo.GetFollowing(userId);

            List<PostAndImages> postAndImages = new List<PostAndImages>();

            foreach(var post in posts)
            {
                var postAndImage = new PostAndImages()
                {
                    PostId = post.PostId,
                    UserId = post.UserId,
                    Description = post.Description,
                    Likes = post.Likes,
                    userName = user.Username

                };

                postAndImage.images = await _imageRepo.GetImages(post.PostId);


                foreach (var image in postAndImage.images)
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

            return new ProfileResponse() { 
                UserName = user.Username,
                PostAndImages= postAndImages,
                PostCount = posts.Count(),
                FollowerCount = followers.Count(),
                FollowingCount = followingList.Count(),
                        
            };
        }

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
