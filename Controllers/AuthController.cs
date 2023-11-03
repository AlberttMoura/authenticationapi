using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.DTOs;
using MyAPI.Models;

namespace MyAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly DataContextDapper _dapper;
    private readonly DataContextEF _ef;
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config)
    {
      this._dapper = new DataContextDapper(config);
      this._ef = new DataContextEF(config);
      this._config = config;
    }

    [HttpPost("Register")]
    public IActionResult Register(UserRegister userRegister)
    {
      Auth? existingAuth = this._ef.Auth.FirstOrDefault(a => a.Email == userRegister.Email);

      if (existingAuth != null)
      {
        throw new Exception("User already exists");
      }

      byte[] passwordSalt = new byte[128 / 8];

      using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
      {
        rng.GetNonZeroBytes(passwordSalt);
      }

      byte[] passwordHash = this.GetPasswordHash(userRegister.Password, passwordSalt);

      if (this._ef.Auth.Add(new Auth() { Email = userRegister.Email, PasswordHash = passwordHash, PasswordSalt = passwordSalt }) != null
      && this._ef.Users.Add(new User() { Email = userRegister.Email, Username = userRegister.Username }) != null)
      {
        this._ef.SaveChanges();
        return Ok("User registered successfully");
      }
      throw new Exception("Failed to register");
    }

    [HttpPost("Login")]
    public IActionResult Login(UserLogin userLogin)
    {
      Auth? existingAuth = this._ef.Auth.FirstOrDefault(a => a.Email == userLogin.Email) ?? throw new Exception("User doesn't exist");

      byte[] passwordHash = this.GetPasswordHash(userLogin.Password, existingAuth.PasswordSalt);

      for (int index = 0; index < passwordHash.Length; index++)
      {
        if (passwordHash[index] != existingAuth.PasswordHash[index])
        {
          return StatusCode(401, "Incorrect password");
        }
      }

      return Ok();
    }

    private byte[] GetPasswordHash(string password, byte[] passwordSalt)
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
  }
}