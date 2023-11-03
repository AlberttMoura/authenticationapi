using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.DTOs;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserController(IConfiguration config, IUserRepository userRepository)
    {
      this._userRepository = userRepository;
      this._mapper = new Mapper(new MapperConfiguration(cfg => { cfg.CreateMap<UserEdit, User>(); cfg.CreateMap<UserRegister, User>(); }));
    }

    [HttpGet]
    public IEnumerable<User> UserFindAll()
    {
      IEnumerable<User> users = this._userRepository.FindAll();
      return users;
    }

    [HttpGet("{id}")]
    public User? UserFindById(long id)
    {
      User? user = this._userRepository.FindById(id);
      return user;
    }

    [HttpPut]
    public bool EditUser(UserEdit user)
    {
      return this._userRepository.Update(this._mapper.Map<User>(user));
    }

    [HttpPost]
    public bool AddUser(UserRegister newUser)
    {
      return newUser.Password == newUser.ConfirmationPassword && this._userRepository.Add(this._mapper.Map<User>(newUser));
    }

    [HttpDelete]
    public bool DeleteUser(long id)
    {
      return this._userRepository.Delete(id);
    }
  }
}