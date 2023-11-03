namespace MyAPI.DTOs
{
  public partial class PostEdit
  {
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
  }
}