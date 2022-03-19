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
    [Authorize]
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
        public async Task<IActionResult> PostToken(LoginDto loginDto)
        {
            if (loginDto.Username == null! || loginDto.Password == null!) return BadRequest();

            var user = await _context.Users
                .Where(user => user.Username.Equals(loginDto.Username) && user.Password.Equals(loginDto.Password))
                .FirstOrDefaultAsync();

            if (user == null) return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Claims = new Dictionary<string, object>
                {
                    ["id"] = user.Id
                }
            });

            var jwt = new
            {
                token = tokenHandler.WriteToken(securityToken)
            };

            Response.Cookies.Append("diary-log-jwt", jwt.token, new CookieOptions
            {
                IsEssential = true
            });

            return Ok(jwt);
        }

        [HttpGet]
        public IActionResult GetLoggedIn()
        {
            return Ok();
        }
    }
}