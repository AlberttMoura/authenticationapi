using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPI.Models
{
  [Table("auth")]
  public partial class Auth
  {
    [Key]
    [Column("user_email")]
    public string Email { get; set; } = "";
    [Column("auth_password_hash")]
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    [Column("auth_password_salt")]
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    [Column("auth_role")]
    public string Role { get; set; } = "Common";
  }
}