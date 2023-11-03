using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPI.Models
{
  [Table("post")]
  public partial class Post
  {
    [Key]
    [Column("post_id")]
    public long Id { get; set; }
    [Column("user_id")]
    public long UserId { get; set; }

    [Column("post_title")]
    public string Title { get; set; } = "";
    [Column("post_content")]
    public string Content { get; set; } = "";
    [Editable(false)]
    [Column("post_creation_date")]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    [Column("post_update_date")]
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
  }
}