namespace MyAPI.DTOs
{
  public partial class UserEdit
  {
    public long Id { get; set; }
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";

  }
}