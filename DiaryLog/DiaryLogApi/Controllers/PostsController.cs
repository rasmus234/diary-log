#nullable disable
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DiaryLogDomain;
using DiaryLogDomain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace DiaryLogApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly DiaryLogContext _context;
    private readonly IConfigurationProvider _mapConfig;
    private readonly IMapper _mapper;

    public PostsController(DiaryLogContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _mapConfig = mapper.ConfigurationProvider;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
    {
        return await _context.Posts.ProjectTo<PostDto>(_mapConfig).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        var post = await _context.Posts.ProjectTo<PostDto>(_mapConfig).FirstOrDefaultAsync(p => p.Id == id);

        if (post == null) return NotFound("Post not found");

        return post;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPost(int id, PostDto postDto)
    {
        var post = _mapper.Map<Post>(postDto);
        if (id != post.Id) return BadRequest();

        _context.Entry(post).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> PostPost(CreatePostDto createPostDto)
    {
        var post = _mapper.Map<Post>(createPostDto);
        post.Date = DateTime.Now;

        _context.Posts.Add(post);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (PostExists(post.Id))
                return Conflict();
            throw;
        }

        var postDto = _mapper.Map<PostDto>(post);
        return CreatedAtAction("GetPost", new {id = post.Id}, postDto);
    }

    // DELETE: api/Post/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return NotFound();

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PostExists(int id)
    {
        return _context.Posts.Any(e => e.Id == id);
    }
}