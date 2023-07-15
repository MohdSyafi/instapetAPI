using instapetService.ServiceModel;


namespace instapetService.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileResponse> GetProfile(int userId);
    }
}
