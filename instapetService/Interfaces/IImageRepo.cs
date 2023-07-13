using instapetService.Models;


namespace instapetService.Interfaces
{
    public interface IImageRepo
    {
        Task<List<Image>> GetImages(int postId);

        Task<bool> AddImages(List<Image> Images);
    }
}
