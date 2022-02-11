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
public class CategoriesController : ControllerBase
{
    private readonly DiaryLogContext _context;
    private readonly IConfigurationProvider _mapConfig;
    private readonly IMapper _mapper;

    public CategoriesController(DiaryLogContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _mapConfig = mapper.ConfigurationProvider;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        return await _context.Categories.ProjectTo<CategoryDto>(_mapConfig).ToListAsync();
    }

    // GET: api/Categories/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _context.Categories.ProjectTo<CategoryDto>(_mapConfig)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null) return NotFound();

        return category;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutCategory(int id, CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        if (id != category.Id) return BadRequest();

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto categoryDto)
    {
        //check if user has category with same name
        var categoryExists = await _context.Categories.AnyAsync(c =>
            c.CategoryName == categoryDto.CategoryName && c.UserId == categoryDto.UserId);
        if (categoryExists) return BadRequest("Category with this name already exists");
        
        var category = _mapper.Map<Category>(categoryDto);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        categoryDto = _mapper.Map<CategoryDto>(category);

        return CreatedAtAction("GetCategory", new {id = category.Id}, categoryDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}