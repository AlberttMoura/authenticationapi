using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {

    private readonly DataContextEF _ef;
    private readonly IMapper _mapper;
    public UserController(IConfiguration config)
    {
      _ef = new DataContextEF(config);
      this._mapper = new Mapper(new MapperConfiguration(cfg => { cfg.CreateMap<UserEdit, User>(); cfg.CreateMap<UserAdd, User>(); }));
    }

    [HttpGet]
    public IEnumerable<User> UserFindAll()
    {
      IEnumerable<User> users = this._ef.Users.OrderBy(user => user.Id);
      return users;
    }

    [HttpGet("{id}")]
    public User? UserFindById(long id)
    {
      User? user = this._ef.Users.Find(id);
      return user;
    }

    [HttpPut]
    public IActionResult EditUser(UserEdit user)
    {
      User userDb = this._mapper.Map<User>(user);
      this._ef.Update(userDb);
      if (this._ef.SaveChanges() > 0)
      {
        return Ok();
      }
      throw new Exception($"Failed to Update User: {user}");
    }

    [HttpPost]
    public IActionResult AddUser(UserAdd newUser)
    {
      User userDb = this._mapper.Map<User>(newUser);
      this._ef.Add(userDb);
      if (this._ef.SaveChanges() > 0)
      {
        return Ok();
      }
      throw new Exception($"Failed to Add User: {newUser}");
    }

    [HttpDelete]
    public IActionResult DeleteUser(long id)
    {
      User? userDb = this._ef.Users.Find(id);
      if (userDb != null)
      {
        this._ef.Remove(userDb);
      }
      if (this._ef.SaveChanges() > 0)
      {
        return Ok();
      }
      throw new Exception($"Failed to delete user with id: {id}");
    }
  }
}