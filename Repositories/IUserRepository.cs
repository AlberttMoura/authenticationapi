using MyAPI.Models;

namespace MyAPI.Repositories
{
  public interface IUserRepository
  {
    public List<User> FindAll();
    public User? FindById(long id);
    public bool SaveChanges();
    public bool Add(User newUser);
    public bool Update(User user);
    public bool Delete(long id);
  }
}