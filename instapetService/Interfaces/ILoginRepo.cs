using instapetService.Models;


namespace instapetService.Interfaces
{
    public interface ILoginRepo
    {
        Task<User> GetUser(string searchInput);
        Task<bool> AddUser(User user);
    }
}
