using instapetService.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.ServiceModel
{
    public class PostAndImages : Post
    {
        public string userName { get; set; } = string.Empty;
        public List<IFormFile>? formFiles { get; set; }
        public List<Image> images { get; set; } = new List<Image>();
    }
}
