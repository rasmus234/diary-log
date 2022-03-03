#nullable disable
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DiaryLogDomain;
using DiaryLogDomain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace DiaryLogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DiaryLogContext _context;
    private readonly IConfigurationProvider _mapConfig;
    private readonly IMapper _mapper;

    public UsersController(DiaryLogContext context, IMapper mapper, IConfigurationProvider mapConfig)
    {
        _context = context;
        _mapper = mapper;
        _mapConfig = mapConfig;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return await _context.Users.ProjectTo<UserDto>(_mapConfig).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.ProjectTo<UserDto>(_mapConfig).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return NotFound("User not found");

        
        return user;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutUser(int id, UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        if (id != user.Id) return BadRequest();

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
                return NotFound("User not found");
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        if (await _context.Users.AnyAsync(userFromDb => user.Username.Equals(userFromDb.Username)))
            return BadRequest("Username already exists");

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        userDto = _mapper.Map<UserDto>(user);
        return CreatedAtAction("GetUser", new {id = user.Id}, userDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound("User not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}