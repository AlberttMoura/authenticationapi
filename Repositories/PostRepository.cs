using MyAPI.Data;
using MyAPI.DTOs;
using MyAPI.Models;

namespace MyAPI.Repositories
{
  public class PostRepository : IPostRepository
  {
    private readonly DataContextEF _ef;
    public PostRepository(IConfiguration config)
    {
      this._ef = new DataContextEF(config);
    }
    public bool Add(Post post)
    {
      this._ef.Posts.Add(post);
      return this.SaveChanges();
    }

    public bool Delete(long postId)
    {
      Post? post = this._ef.Posts.Find(postId);
      if (post != null)
      {
        this._ef.Remove(post);
      }
      return this.SaveChanges();
    }

    public List<Post> FindAll()
    {
      List<Post> posts = this._ef.Posts.ToList();
      return posts;
    }

    public Post? FindById(long postId)
    {
      Post? post = this._ef.Posts.Find(postId);
      return post;
    }

    public List<Post> FindByUser(long userId)
    {
      List<Post> posts = this._ef.Posts.Where(p => p.UserId == userId).ToList();
      return posts;
    }

    public bool SaveChanges()
    {
      return this._ef.SaveChanges() > 0;
    }

    public bool Update(Post post)
    {
      this._ef.Posts.Update(post);
      return this.SaveChanges();
    }
  }
}