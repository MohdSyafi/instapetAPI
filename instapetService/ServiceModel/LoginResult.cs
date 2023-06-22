using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.ServiceModel
{
    public class LoginResult
    {
        public bool IsAuthenticated { get; set; }

        public int UserId { get; set; } 

    }
}
