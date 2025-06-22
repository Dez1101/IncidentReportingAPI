using IncidentReportingApi.Dto;
using IncidentReportingApi.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;


namespace IncidentReportingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Microsoft.AspNet.Identity.EntityFramework.IdentityUser> _userManager;
        private readonly RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> _roleManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<Microsoft.AspNet.Identity.EntityFramework.IdentityUser> userManager,
            RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole> roleManager,
            ITokenRepository tokenRepository)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if (registerRequestDto == null ||
                string.IsNullOrWhiteSpace(registerRequestDto.Username) ||
                string.IsNullOrWhiteSpace(registerRequestDto.Password))
            {
                return BadRequest(new { Message = "Username and password are required." });
            }

            if (!IsValidEmail(registerRequestDto.Username))
            {
                return BadRequest(new { Message = "Invalid email format." });
            }

            var identityUser = new Microsoft.AspNet.Identity.EntityFramework.IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (!identityResult.Succeeded)
            {
                return BadRequest(new
                {
                    Message = "User creation failed.",
                    Errors = identityResult.Errors.Select(e => e.Description)
                });
            }

            if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
            {
                foreach (var role in registerRequestDto.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        return BadRequest(new { Message = $"Role '{role}' does not exist." });
                    }
                }

                var roleResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new
                    {
                        Message = "Failed to assign roles.",
                        Errors = roleResult.Errors.Select(e => e.Description)
                    });
                }
            }

            return Ok(new { Message = "User registered successfully." });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null ||
                string.IsNullOrWhiteSpace(loginDto.Username) ||
                string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest(new { Message = "Username and password are required." });
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Username);
            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var jwtToken = await _tokenRepository.CreateJWTokenAsync(user, roles.ToList());

            return Ok(new
            {
                Message = "Login successful.",
                Token = jwtToken
            });
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}