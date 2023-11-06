using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.DTOs;
using MyAPI.Helpers;
using MyAPI.Models;

namespace MyAPI.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly DataContextEF _ef;
    private readonly AuthHelper _authHelper;
    public AuthController(IConfiguration config)
    {
      this._ef = new DataContextEF(config);
      this._authHelper = new(config);
    }

    [AllowAnonymous]
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

      byte[] passwordHash = this._authHelper.GetPasswordHash(userRegister.Password, passwordSalt);

      if (this._ef.Auth.Add(new Auth() { Email = userRegister.Email, PasswordHash = passwordHash, PasswordSalt = passwordSalt }) != null
      && this._ef.Users.Add(new User() { Email = userRegister.Email, Username = userRegister.Username }) != null)
      {
        this._ef.SaveChanges();
        return Ok("User registered successfully");
      }
      throw new Exception("Failed to register");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login(UserLogin userLogin)
    {
      Auth existingAuth = this._ef.Auth.FirstOrDefault(a => a.Email == userLogin.Email) ?? throw new Exception("User not registered");

      byte[] passwordHash = this._authHelper.GetPasswordHash(userLogin.Password, existingAuth.PasswordSalt);

      for (int index = 0; index < passwordHash.Length; index++)
      {
        if (passwordHash[index] != existingAuth.PasswordHash[index])
        {
          return StatusCode(401, "Incorrect password");
        }
      }

      User user = this._ef.Users.FirstOrDefault(u => u.Email == userLogin.Email) ?? throw new Exception("User could not be found");

      return Ok(new Dictionary<string, string> {
        {"token", this._authHelper.CreateToken(user.Id, existingAuth.Role)}
      });
    }

    [HttpGet("RefreshToken")]
    public string RefreshToken()
    {
      string userId = User.FindFirst("userId")?.Value ?? throw new Exception("User Id could not be found");
      User user = this._ef.Users.Find(long.Parse(userId)) ?? throw new Exception("User could not be found");
      Auth auth = this._ef.Auth.FirstOrDefault(a => a.Email == user.Email) ?? throw new Exception("User not registered");
      return this._authHelper.CreateToken(user.Id, auth.Role);
    }
  }
}