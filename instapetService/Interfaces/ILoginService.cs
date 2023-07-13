using instapetService.Models;
using instapetService.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResult> Login(User InputUser);

        Task<SignupResult> Signup(User user);
    }
}
