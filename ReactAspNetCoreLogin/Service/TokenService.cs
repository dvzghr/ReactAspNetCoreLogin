using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using ReactAspNetCoreLogin.Models;

namespace ReactAspNetCoreLogin.Service
{
    public class TokenService
    {
        public (string, DateTime) GetToken(UserLoginViewModel user)
        {
            var claims = new[]
                         {
                             //new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                             new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                             new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                             new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture))
                         };

            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = Encoding.ASCII.GetBytes("dotnetcoreoauthsecret");
            var securityKey = new SymmetricSecurityKey(secretKey);
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: "dotnetcoreoauth",
                                             audience: "dotnetcoreoauth",
                                             claims: claims,
                                             expires: DateTime.Now.AddSeconds(30),
                                             signingCredentials: signingCred);

            return (tokenHandler.WriteToken(token), token.ValidTo);
        }
    }
}