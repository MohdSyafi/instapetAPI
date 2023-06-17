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

    public interface IImageRepo
    {
        Task<List<Image>> GetImages(int postId);

        Task<bool> AddImages(List<Image> Images);
    }
    public class ImageRepo : IImageRepo
    {
        private InstaPetContext _db;
        public ImageRepo(InstaPetContext instaPetContext) { 
            _db = instaPetContext;
        }
        public async Task<bool> AddImages(List<Image> Images)
        {
            try
            {
                await _db.Image.AddRangeAsync(Images);
                await _db.SaveChangesAsync();
                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Image>> GetImages(int postId)
        {
            var ImageList = await _db.Image.Where(x=>x.PostId == postId).ToListAsync();    

            return ImageList;
        }
    }
}
