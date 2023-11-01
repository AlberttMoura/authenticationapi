using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {

    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
      _dapper = new DataContextDapper(config);
    }

    [HttpGet(Name = "UserFindAll")]
    public IEnumerable<User> UserFindAll()
    {
      string sql = "SELECT * FROM \"user\";";
      IEnumerable<User> users = this._dapper.LoadData<User>(sql);
      return users;
    }

    [HttpGet("{id}")]
    public User? UserFindById(long id)
    {
      string sql = $"SELECT * FROM \"user\" WHERE id = {id};";
      User? user = this._dapper.LoadDataSingle<User?>(sql);
      return user;
    }

    [HttpPut]
    public IActionResult EditUser(UserEdit user)
    {
      string sql = $"UPDATE \"user\" SET username = '{user.Username}', email = '{user.Email}', \"password\" = '{user.Password}' WHERE id = {user.Id};";
      if (this._dapper.ExecuteSql(sql))
      {
        return Ok();
      }
      throw new Exception($"Failed to update user with id: {user.Id}");
    }

    [HttpPost]
    public IActionResult AddUser(UserAdd newUser)
    {
      string sql = $"INSERT INTO \"user\" (username, email, \"password\") VALUES ('{newUser.Username}', '{newUser.Email}', '{newUser.Password}');";
      if (this._dapper.ExecuteSql(sql))
      {
        return Ok();
      }
      throw new Exception($"Failed to create a new new user: {newUser}");
    }
  }
}