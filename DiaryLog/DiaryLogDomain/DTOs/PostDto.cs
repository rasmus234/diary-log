namespace DiaryLogDomain.DTOs;

public class PostDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    
    public virtual ICollection<CommentDto> Comments { get; set; }
    public virtual ICollection<PostCategoryDto> PostCategories { get; set; }
    public virtual ICollection<RatingDto> Ratings { get; set; }
}