namespace MyAPI.Models
{
  public partial class UserEdit
  {
    public long Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
  }
}