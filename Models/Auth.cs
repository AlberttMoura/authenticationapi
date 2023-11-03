using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPI.Models
{
  [Table("auth")]
  public class Auth
  {
    [Key]
    [Column("email")]
    public string Email { get; set; } = "";
    [Column("password_hash")]
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    [Column("password_salt")]
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
  }
}