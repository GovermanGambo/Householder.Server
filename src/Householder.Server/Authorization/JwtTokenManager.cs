using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Householder.Server.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Householder.Server.Authorization
{
    public class JwtTokenManager : ITokenManager
    {
        private readonly IConfiguration configuration;

        public JwtTokenManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public string GenerateToken(UserLoginDTO userDTO)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}