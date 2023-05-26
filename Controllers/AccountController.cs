using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using shop.Models;
using AutoMapper;
using shop.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(AppDbContext context, UserManager<ApiUser> userManager, ILogger<AccountController> logger, IMapper mapper, IAuthManager authManager)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiUser>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .ToListAsync();
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiUser>> GetApiUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var apiUser = await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

            // FindAsync(id);

            if (apiUser == null)
            {
                return NotFound();
            }

            return apiUser;
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApiUser(string id, ApiUser apiUser)
        {
            if (id != apiUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(apiUser).State = EntityState.Modified;

            var user = await _context.Users
                .Include(u => u.UserRoles).
                ThenInclude(r => r.RoleId).
                FirstOrDefaultAsync(u => u.Id == id);

            List<string> roleList = new List<string>();
            foreach (var role in user.UserRoles)
            {
                roleList.Add(role.Role.Name);
            }

            string[] roleArray = roleList.ToArray();

            await _userManager.RemoveFromRolesAsync(user, roleArray);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApiUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                // return ;
                if (!result.Succeeded)
                {
                    foreach (var e in result.Errors)
                    {
                        ModelState.AddModelError(e.Code, e.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);

                return Accepted();
                // return CreatedAtAction("GetApiUser", new { id = user.Id }, user);
            }
            catch (System.Exception)
            {
                return Problem("Something went wrong", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _authManager.ValidateUser(userDTO.Email);
                var pass = await _userManager.CheckPasswordAsync(user, userDTO.Password);

                if (user == null || pass == false)
                {
                    if (Request.Cookies["userName"] != null)
                    {
                        Response.Cookies.Delete("userName");
                    }
                    if (Request.Cookies["refreshToken"] != null)
                    {
                        Response.Cookies.Delete("refreshToken");
                    }

                    return Unauthorized();
                }

                var roles = await _userManager.GetRolesAsync(user);

                var refreshToken = await _authManager.CreateRefreshToken();

                var cookieOptions = new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                Response.Cookies.Append(
                    "userName",
                    user.Email,
                    cookieOptions
                );
                Response.Cookies.Append(
                    "refreshToken",
                    refreshToken,
                    cookieOptions
                );

                return Accepted(new
                {
                    Token = await _authManager.CreateToken(),
                    Roles = roles
                });
            }
            catch (System.Exception)
            {
                return Problem($"Something went wrong {nameof(Login)}", statusCode: 500);
            }
        }

        [HttpGet("refresh")]
        public async Task<ActionResult> GetRefreshToken()
        {
            if (!(Request.Cookies.TryGetValue("userName", out var userName) && Request.Cookies.TryGetValue("refreshToken", out var refreshToken)))
                return BadRequest();

            var user = await _authManager.ValidateUser(userName);

            if (user == null)
                return BadRequest();

            // var isValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, "RefreshToken", refreshToken);

            var isValid = _authManager.ValidateRefreshToken(refreshToken, userName);

            if (!isValid)
                return BadRequest();

            return Accepted(new
            {
                Token = await _authManager.CreateToken(),
                Roles = await _userManager.GetRolesAsync(user)
            });
        }


        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            if (Request.Cookies["userName"] != null)
            {
                var email = Request.Cookies["userName"];
                var user = await _authManager.ValidateUser(email);
                await _userManager.RemoveAuthenticationTokenAsync(user, TokenOptions.DefaultProvider, "RefreshToken");
                await _userManager.UpdateSecurityStampAsync(user);

                Response.Cookies.Delete("userName");
            }

            if (Request.Cookies["refreshToken"] != null)
            {
                Response.Cookies.Delete("refreshToken");
            }

            return Ok();
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApiUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var apiUser = await _context.Users.FindAsync(id);
            if (apiUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(apiUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApiUserExists(string id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
