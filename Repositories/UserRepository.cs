using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContextEF _ef;
    public UserRepository(IConfiguration config)
    {
      this._ef = new DataContextEF(config);
    }

    public List<User> FindAll()
    {
      List<User> users = this._ef.Users.OrderBy(user => user.Id).ToList();
      return users;
    }

    public User? FindById(long id)
    {
      User? user = this._ef.Users.Find(id);
      return user;
    }

    public bool SaveChanges()
    {
      return this._ef.SaveChanges() > 0;
    }

    public bool Add(User newUser)
    {
      this._ef.Users.Add(newUser);
      return this.SaveChanges();
    }

    public bool Update(User user)
    {
      this._ef.Users.Update(user);
      return this.SaveChanges();
    }

    public bool Delete(long id)
    {
      User? userDb = this._ef.Users.Find(id);
      if (userDb != null)
      {
        this._ef.Remove(userDb);
      }
      return this.SaveChanges();
    }
  }
}