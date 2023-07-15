using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.ServiceModel
{
    public class ProfileResponse
    {
        public string UserName { get; set; } = string.Empty;

        public int PostCount { get; set; } 

        public int FollowerCount { get; set; }

        public int FollowingCount { get; set; }

        public string ProfileDescription { get; set; } = string.Empty;
       
        public List<PostAndImages> PostAndImages { get; set; } = new List<PostAndImages>();

    }
}
