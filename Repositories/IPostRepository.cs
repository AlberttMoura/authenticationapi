using MyAPI.DTOs;
using MyAPI.Models;

namespace MyAPI.Repositories
{
  public interface IPostRepository
  {
    public List<Post> FindAll();
    public Post? FindById(long postId);
    public List<Post> FindByUser(long userId);
    public bool SaveChanges();
    public bool Add(Post post);
    public bool Update(Post post);
    public bool Delete(long postId);
  }
}