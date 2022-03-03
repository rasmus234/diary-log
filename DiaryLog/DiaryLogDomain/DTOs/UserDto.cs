namespace DiaryLogDomain.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
}