using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.ServiceModel
{
    public class SignupResult
    {
        public SignupResult() { }

        public SignupResult(string msg="",bool signedUp=true) {
            message = msg;
            isSignedUp = signedUp;
        }
        public string message { get; set; } = "";
        public bool isSignedUp { get; set; } = true;
    }

}
