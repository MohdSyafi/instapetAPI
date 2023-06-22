using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.ServiceModel
{
    public class SearchResult
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public bool Followed { get; set; }

    }
}
