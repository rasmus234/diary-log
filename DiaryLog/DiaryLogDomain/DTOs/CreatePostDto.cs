namespace DiaryLogDomain.DTOs;

public class CreatePostDto
{
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}