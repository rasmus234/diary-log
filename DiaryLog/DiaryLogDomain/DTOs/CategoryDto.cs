namespace DiaryLogDomain.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CategoryName { get; set; } = null!;
}