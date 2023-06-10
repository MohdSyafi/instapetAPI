using instapetService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Services
{

    public interface ILoginService
    {
        bool Login(string username, string password);
    }

    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _loginRepo;

        public LoginService(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        public bool Login(string username, string password)
        {
            return _loginRepo.IsAuthenticated(username, password);
        }
    }
}
