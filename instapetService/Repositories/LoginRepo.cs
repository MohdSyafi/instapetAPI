using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Repositories
{
    public interface ILoginRepo
    {
        bool IsAuthenticated(string username, string password);
    }
    public class LoginRepo : ILoginRepo
    {
        public LoginRepo() { }
        public bool IsAuthenticated(string username, string password)
        {
            return true;
        }
    }
}
