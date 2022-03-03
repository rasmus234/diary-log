using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DiaryLogDomain;
using DiaryLogDomain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DiaryLogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DiaryLogContext _context;

        public AuthenticationController(IConfiguration configuration, DiaryLogContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // POST: api/Authentication
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> GetToken(LoginDto loginDto)
        {
            if (loginDto.Username == null! || loginDto.Password == null!) return BadRequest();

            var user = await _context.Users.Where(user => user.Username.Equals(loginDto.Username) && user.Password.Equals(loginDto.Password)).FirstOrDefaultAsync();

            if (user == null) return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                // Claims = new Dictionary<string, object> { }
            });

            var jwt = new
            {
                token = tokenHandler.WriteToken(securityToken)
            };

            return Ok(jwt);
        }
    }
}