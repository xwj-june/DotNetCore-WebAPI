using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp
{
    public class CustomeToekAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            //var userName = ""; //not authenticated
            var userName = "june"; //authenticated
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var claim = new Claim(ClaimTypes.Name, userName);
                var identity = new ClaimsIdentity(new[] { claim }, "Custom Token Auth");
                var principle = new ClaimsPrincipal(identity);

                return Task.FromResult(new AuthenticationState(principle));
            }
            else
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }
        }
    }
}
