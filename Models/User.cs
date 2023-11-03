using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPI.Models
{
  [Table("user")]
  public partial class User
  {
    [Key]
    [Column("user_id")]
    public long Id { get; set; }
    [Column("user_nick")]
    public string Username { get; set; } = "";
    [Column("user_email")]
    public string Email { get; set; } = "";
    [Column("user_ativo")]
    public int Ativo { get; set; } = 1;
    [Editable(false)]
    [Column("user_creation_date")]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
  }
}