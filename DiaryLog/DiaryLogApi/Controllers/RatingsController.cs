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
public class RatingsController : ControllerBase
{
    private readonly DiaryLogContext _context;
    private readonly IMapper _mapper;
    private readonly IConfigurationProvider _mapConfig;

    public RatingsController(DiaryLogContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _mapConfig = mapper.ConfigurationProvider;
    }

    // GET: api/Ratings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatings()
    {
        return await _context.Ratings.ProjectTo<RatingDto>(_mapConfig).ToListAsync();
    }

    // GET: api/Ratings/5
    [HttpGet("{userId:int}, {postId:int}")]
    public async Task<ActionResult<RatingDto>> GetRating(int userId, int postId)
    {
        var rating = await _context.Ratings.ProjectTo<RatingDto>(_mapConfig)
            .FirstOrDefaultAsync(r => r.UserId == userId && r.PostId == postId);

        if (rating == null) return NotFound();

        return rating;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutRating(int id, RatingDto ratingDto)
    {
        
        var rating = _mapper.Map<Rating>(ratingDto);
        
        if (id != rating.UserId) return BadRequest();

        _context.Entry(rating).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RatingExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Rating>> PostRating(RatingDto ratingDto)
    {
        var rating = _mapper.Map<Rating>(ratingDto);
        
        _context.Ratings.Add(rating);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (RatingExists(rating.UserId))
                return Conflict("Rating already exists");
            throw;
        }
        ratingDto = _mapper.Map<RatingDto>(rating);

        return CreatedAtAction("GetRating", new {id = rating.UserId}, ratingDto);
    }

    // DELETE: api/Ratings/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRating(int id)
    {
        var rating = await _context.Ratings.FindAsync(id);
        if (rating == null) return NotFound();

        _context.Ratings.Remove(rating);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RatingExists(int id)
    {
        return _context.Ratings.Any(e => e.UserId == id);
    }
}