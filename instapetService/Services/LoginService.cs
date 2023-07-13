using Amazon.Runtime;
using instapetService.Interfaces;
using instapetService.Models;
using instapetService.Repositories;
using instapetService.ServiceModel;


namespace instapetService.Services
{

    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _loginRepo;

        public LoginService(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        public async Task<LoginResult>  Login(User InputUser)
        {
            var user =  await _loginRepo.GetUser(InputUser.Username);

            if (user == null)
                return new LoginResult() { IsAuthenticated = false, UserId = 0 };

            if (user.Password != InputUser.Password)
                return new LoginResult() { IsAuthenticated = false, UserId = 0 };

            return new LoginResult() { IsAuthenticated = true, UserId = user.Id};
        }

        public async Task<SignupResult> Signup(User user)
        {
            try
            {
                var userExist = await _loginRepo.GetUser(user.Username);

                if (userExist != null)
                    return new SignupResult("user already exist", false);

                var isAddSucces = await _loginRepo.AddUser(user);

                if (!isAddSucces)
                    return new SignupResult("unexpected error when adding user", false);

            }
            catch(Exception ex)
            {
                return new SignupResult(ex.ToString(), false);
            }


            return new SignupResult();

        }
    }
}
