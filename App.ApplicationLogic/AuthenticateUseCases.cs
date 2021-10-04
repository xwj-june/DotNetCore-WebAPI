using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class AuthenticateUseCases : IAuthenticateUseCases
    {
        private readonly IAuthenticationRepository authenticationRepository;
        public AuthenticateUseCases(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }
        public async Task<string> LoginAsync(string userName, string password)
        {
            return await authenticationRepository.LoginAsync(userName, password);
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            return await authenticationRepository.GetUserInfoAsync(token);

        }
    }
}
