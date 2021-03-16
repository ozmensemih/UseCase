using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;

namespace UseCase.Common
{
    public static class CommonFactory
    {
        private static Random Rnd;
        static CommonFactory()
        {
            Rnd = new Random();
        }
        public static string GetJwtToken(string Id, string key1, string audience, string issuer, string expires, string userName, List<string> roles = default)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(key1);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString())

                };
            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = audience,
                Issuer = issuer,
                Subject = new ClaimsIdentity(claims),

                //Expire token after some time
                Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(expires)),

                //Let's also sign token credentials for a security aspect, this is important!!!
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString.ToString();
        }
    }
}
