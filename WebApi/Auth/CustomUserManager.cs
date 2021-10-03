using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        private Dictionary<string, string> credentials = new Dictionary<string, string>()
        {
            { "june", "password"},
            { "liz", "password1"}
        };

        private readonly ICustomTokenManager customTokenManager;

        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            this.customTokenManager = customTokenManager;
        }
        public string Authenticate(string userName, string password)
        {
            //validate the credentials
            if (credentials[userName] != password)
            {
                return string.Empty;
            }

            //generate token
            return customTokenManager.CreateToken(userName) ;
        }

    }
}
