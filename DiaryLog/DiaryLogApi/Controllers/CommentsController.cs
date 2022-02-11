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
public class CommentsController : ControllerBase
{
    private readonly DiaryLogContext _context;
    private readonly IConfigurationProvider _mapConfig;
    private readonly IMapper _mapper;

    public CommentsController(DiaryLogContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _mapConfig = mapper.ConfigurationProvider;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
    {
        return await _context.Comments.ProjectTo<CommentDto>(_mapConfig).ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CommentDto>> GetComment(int id)
    {
        var comment = await _context.Comments.ProjectTo<CommentDto>(_mapConfig).FirstOrDefaultAsync(x => x.Id == id);

        if (comment == null) return NotFound();

        return comment;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutComment(int id, CommentDto commentDto)
    {
        
        var comment = _mapper.Map<Comment>(commentDto);
        if (id != comment.Id) return BadRequest();

        _context.Entry(comment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CommentExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> PostComment(CommentDto commentDto)
    {
        var comment = _mapper.Map<Comment>(commentDto);
        _context.Comments.Add(_mapper.Map<Comment>(comment));
        await _context.SaveChangesAsync();
        commentDto = _mapper.Map<CommentDto>(comment);

        return CreatedAtAction("GetComment", new {id = comment.Id}, commentDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null) return NotFound();

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CommentExists(int id)
    {
        return _context.Comments.Any(e => e.Id == id);
    }
}