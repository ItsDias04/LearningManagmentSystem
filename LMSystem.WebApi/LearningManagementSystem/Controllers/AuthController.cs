using LearningManagementSystem.Data;
using LearningManagementSystem.DTO;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public AuthController(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("gettoken")]
    public IActionResult GetToken(string role, string username)
    {
        if (role == "tutor") { return Ok(GenerateToken(username, "Tutor")); }
        if (role == "student") { return Ok(GenerateToken(username, "Student")); }

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO loginData)
    {
        var username = loginData.username;
        var password = loginData.password;
        Console.WriteLine("username:" + username);
        Console.WriteLine("password: "+ password);
        var tutor = _context.Tutor.SingleOrDefault(u => u.Email == username && u.PasswordHash == password);

        if (tutor != null) 
        {
            var tutor_TOKEN = GenerateToken(username, "Tutor");
            return Ok(new { token = tutor_TOKEN, role = "tutor" });

        }
        var student = _context.Student.SingleOrDefault(u => u.Email == username && u.PasswordHash == password);
        
        if (student != null)
        {
            var token_student = GenerateToken(username, "Student");
            return Ok(new { token = token_student,  role = "student" });
        }

        return Unauthorized();
    }
    
    private TokenModel GenerateToken(string username, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role),
            //new Claim("SchoolName", schoolName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddHours(5),
            signingCredentials: creds);

        return new TokenModel
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        };
    }
}
