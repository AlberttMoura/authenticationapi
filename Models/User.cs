using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPI.Models
{
  [Table("user")]
  public partial class User
  {
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Column("username")]
    public string Username { get; set; } = "";
    [Column("email")]
    public string Email { get; set; } = "";
    [Column("password")]
    public string Password { get; set; } = "";
    [Column("ativo")]
    public int Ativo { get; set; } = 1;
  }
}