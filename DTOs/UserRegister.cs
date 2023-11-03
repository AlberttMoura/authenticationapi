namespace MyAPI.DTOs
{
  public partial class UserRegister
  {
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string ConfirmationPassword { get; set; } = "";
  }
}