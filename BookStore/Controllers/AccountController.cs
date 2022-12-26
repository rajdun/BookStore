using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AccountController(ILogger<BookController> logger, UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser(User user)
    {
        var dbUser = await _userManager.FindByNameAsync(user.Username);
        if (dbUser != null)
        {
            return BadRequest("User already exists");
        }

        dbUser = new IdentityUser()
        {
            UserName = user.Username
        };

        var result = _userManager.CreateAsync(dbUser, user.Password);

        if (!result.Result.Succeeded)
        {
            _logger.LogError("Failed to create new user " + result.Result.Errors.First().Description);
            return BadRequest(result.Result.Errors.First().Description);
        }
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<Login>> Authenticate(User user)
    {
        var dbUser = await _userManager.FindByNameAsync(user.Username);

        if (dbUser == null)
            return BadRequest("Wrong password or username.");
        
        var hasher = new PasswordHasher<IdentityUser>();
        var verificationResult = hasher.VerifyHashedPassword(dbUser, dbUser.PasswordHash, user.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
            return BadRequest("Wrong password or username.");

        var login = this.GetTokenResponse(dbUser);

        return login;
    }

    private Login GetTokenResponse(IdentityUser user)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expirationDate = DateTime.UtcNow.AddHours(2);
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
            }),
            Expires = expirationDate,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        var result = new Login()
        {
            Expires = expirationDate,
            Token = stringToken
        };
        
        return result;
    }
}