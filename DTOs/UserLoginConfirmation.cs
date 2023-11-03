namespace MyAPI.DTOs
{
  public partial class UserLoginConfirmation
  {
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
  }
}