using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UseCase.MVC.App_Start
{
    public class UserLoginData
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

    }
    public class AuthBuilder
    {

        public async Task SignInAsync(HttpContext context, UserLoginData userData, List<string> roles = null)
        {

            string userIdentityJSON = JsonConvert.SerializeObject(userData);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, userData.FullName));
            identity.AddClaim(new Claim(ClaimTypes.PrimarySid, userData.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.UserData, userIdentityJSON));

            // Add roles
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            // Sign in
            var principal = new ClaimsPrincipal(identity);
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


        }
        public async Task SignOutAsync(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public UserLoginData GetUserData(HttpContext context)
        {


            UserLoginData result = null;
            ClaimsPrincipal user = context.User;

            if (user != null && user.Identity.IsAuthenticated)
            {
                if (user.Identity is ClaimsIdentity identity && identity.Claims.Any(o => o.Type == ClaimTypes.UserData))
                {

                    Claim claim = identity.Claims.FirstOrDefault(o => o.Type == ClaimTypes.UserData);

                    result = JsonConvert.DeserializeObject<UserLoginData>(claim.Value);
                }
            }

            return result;
        }
    }
}
