using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.DTOs;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class PostController : ControllerBase
  {
    private readonly PostRepository _postRepository;
    public PostController(IConfiguration config)
    {
      this._postRepository = new PostRepository(config);
    }

    [HttpGet]
    public List<Post> PostFindAll()
    {
      return this._postRepository.FindAll();
    }

    [HttpGet("{postId}")]
    public Post? PostFindById(long postId)
    {
      return this._postRepository.FindById(postId);
    }

    [HttpGet("MyPosts")]
    public List<Post> PostFindMyPosts()
    {
      string userId = User.FindFirst("userId")?.Value ?? throw new Exception("User Id could not be found");
      return this._postRepository.FindByUser(long.Parse(userId));
    }

    [HttpPost]
    public bool PostAdd(PostAdd postAdd)
    {
      long userId = long.Parse(User.FindFirst("userId")?.Value ?? throw new Exception("User Id could not be found"));
      return this._postRepository.Add(new Post()
      {
        UserId = userId,
        Title = postAdd.Title,
        Content = postAdd.Content
      });
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete]
    public bool PostDelete(long postId)
    {
      Post? post = this._postRepository.FindById(postId);
      if (post != null)
      {
        return this._postRepository.Delete(postId);
      }
      throw new Exception("This post doesn't exist");
    }

    [HttpPut]
    public bool PostUpdate(PostEdit postEdit)
    {
      Post? post = this._postRepository.FindById(postEdit.Id);
      long userId = long.Parse(User.FindFirst("userId")?.Value ?? throw new Exception("User Id could not be found"));
      if (post != null && post.UserId == userId)
      {
        post.Title = postEdit.Title;
        post.Content = postEdit.Content;
        post.UpdateDate = DateTime.UtcNow;
        post.CreationDate = post.CreationDate.ToUniversalTime();
        return this._postRepository.Update(post);
      }
      throw new Exception("You don't have permission to delete this post");
    }
  }
}