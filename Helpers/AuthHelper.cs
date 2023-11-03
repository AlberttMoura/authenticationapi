using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace MyAPI.Helpers
{
  public class AuthHelper
  {
    private readonly IConfiguration _config;
    public AuthHelper(IConfiguration config)
    {
      this._config = config;
    }

    public byte[] GetPasswordHash(string password, byte[] passwordSalt)
    {
      string passwordSaltPlusString = this._config.GetSection("AppSettings:PasswordKey").Value + Convert.ToBase64String(passwordSalt);
      byte[] passWordHash = KeyDerivation.Pbkdf2(
        password: password,
        salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
        prf: KeyDerivationPrf.HMACSHA512,
        iterationCount: 1000000,
        numBytesRequested: 256 / 8
      );
      return passWordHash;
    }

    public string CreateToken(long userId)
    {
      Claim[] claims = new Claim[] {
        new("userId", userId.ToString())
      };

      SymmetricSecurityKey tokenKey = new(Encoding.UTF8.GetBytes(this._config.GetSection("AppSettings:TokenKey").Value ?? ""));

      SigningCredentials credentials = new(tokenKey, SecurityAlgorithms.HmacSha512Signature);

      SecurityTokenDescriptor descriptor = new()
      {
        Subject = new ClaimsIdentity(claims),
        SigningCredentials = credentials,
        Expires = DateTime.UtcNow.AddHours(1)
      };

      JwtSecurityTokenHandler tokenHandler = new();

      SecurityToken token = tokenHandler.CreateToken(descriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}