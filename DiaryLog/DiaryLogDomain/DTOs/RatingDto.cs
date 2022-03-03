namespace DiaryLogDomain.DTOs;

public class RatingDto
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public bool? IsLike { get; set; }
}