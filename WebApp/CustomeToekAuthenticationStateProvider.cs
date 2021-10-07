using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp
{
    public class CustomeToekAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IAuthenticationRepository authenticationRepository;

        public CustomeToekAuthenticationStateProvider(ITokenRepository tokenRepository, IAuthenticationRepository authenticationRepository)
        {
            this.tokenRepository = tokenRepository;
            this.authenticationRepository = authenticationRepository;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userName = await authenticationRepository.GetUserInfoAsync(await tokenRepository.GetToken());
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var claim = new Claim(ClaimTypes.Name, userName);
                var identity = new ClaimsIdentity(new[] { claim }, "Custom Token Auth");
                var principle = new ClaimsPrincipal(identity);

                return new AuthenticationState(principle);
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}
