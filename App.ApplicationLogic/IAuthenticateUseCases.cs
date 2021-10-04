using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface IAuthenticateUseCases
    {
        Task<string> GetUserInfoAsync(string token);
        Task<string> LoginAsync(string userName, string password);
    }
}