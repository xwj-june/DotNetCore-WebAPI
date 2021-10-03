using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Auth;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomUserManager customUserManager;
        private readonly ICustomTokenManager customTokenManager;

        public AuthController(ICustomUserManager customUserManager, ICustomTokenManager customTokenManager)
        {
            this.customUserManager = customUserManager;
            this.customTokenManager = customTokenManager;
        }
        public Task<string> Authenticate(string userName, string password)
        {
            return Task.FromResult(customUserManager.Authenticate(userName, password));
        }

        public Task<bool> Vertify(string token)
        {
            return Task.FromResult(customTokenManager.VerifyToken(token));
        }

        public Task<string> GetUserInfoByToken(string token)
        {
            return Task.FromResult(customTokenManager.GetUserInfoByToken(token));
        }
    }
}
