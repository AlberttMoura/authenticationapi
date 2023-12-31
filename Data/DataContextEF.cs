using Microsoft.EntityFrameworkCore;
using MyAPI.Models;

namespace MyAPI.Data
{
  public class DataContextEF : DbContext
  {
    private readonly IConfiguration _config;
    public DataContextEF(IConfiguration config)
    {
      this._config = config;
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Auth> Auth { get; set; }
    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlite(this._config.GetConnectionString("DefaultConnection"));
      }
    }
  }
}