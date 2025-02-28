using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication2.Services {
  public class JwtTokenService {
    private readonly IConfiguration configuration;
    private readonly SymmetricSecurityKey symmetricSecurityKey;
    public JwtTokenService(IConfiguration configuration) {
      this.configuration=configuration;
      this.symmetricSecurityKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(configuration["JWT:SignInKey"])
      );
    }
    public string CreateToken(IdentityUser identityUser) {
      List<Claim> claims = new List<Claim> {
        new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
        new Claim(JwtRegisteredClaimNames.GivenName, identityUser.UserName)
      };
      SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddMinutes(1), // jwt token often add 5 minute. so in this case, expires become 6 minutes
        SigningCredentials = credentials,
        Issuer = configuration["JWT:Issuer"],
        Audience = configuration["JWT:Audience"]
      };
      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}
