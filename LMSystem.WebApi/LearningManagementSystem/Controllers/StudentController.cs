using LearningManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using LearningManagementSystem.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(IConfiguration configuration, AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("grades")]
        public IActionResult GetGrades() {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
               ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            var user = _context.Student
                .Include(g=>g.Group)
                .ThenInclude(j=>j.Subjects)
                .ThenInclude(l=>l.Tutor)
                .Include(g=>g.Group)
                .ThenInclude(j=>j.Subjects)
                .ThenInclude(k=>k.Grades)
                .FirstOrDefault(c => c.Email == username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var subjects = user.Group.Subjects.Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                TutorName = s.Tutor.UserName,
                Grades = s.Grades
                    .Where(g => g.StudentId == user.Id)
                    .Select(g => g.gradenum) 
                    .ToList()
            }).ToList();
            return Ok(subjects);

        }
        [HttpGet("profile")]
        public IActionResult GetProfile() {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
               ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            
            Console.WriteLine("Hello world: " + username);

            var user = _context.Student
                .Include(s => s.Group)          
                .ThenInclude(j => j.Subjects)
                .Include(s => s.Group)            
                .ThenInclude(g => g.School)          
                .FirstOrDefault(s => s.Email == username);
            var subjects = new List<string> { };
            foreach (var subject in user.Group.Subjects) { subjects.Add(subject.Name); }
            var userProfile = new
            {
                user.UserName,  
                user.Email,      
                GroupName = user.Group?.Name ?? "No Group Assigned", 
                SchoolName = user.Group?.School?.Name ?? "No School Assigned", 
                subjects,
                user.PhoneNumber,
                user.Bio
            };

            return Ok(userProfile);
        }
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
               ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            //username = User.Identity.Name;
            Console.WriteLine(username);
            var user = _context.Student
                .Include(g=>g.Group.Subjects)
                .ThenInclude(j=> j.Tutor)
                .FirstOrDefault(c => c.Email == username);

            var subjects = user.Group.Subjects.Select(
                g => new
                {
                    g.Name,
                    g.Description,
                    g.Tutor.UserName,
                    g.Tutor.PhoneNumber,
                    g.Tutor.Email
                });

            return Ok(subjects);
        }
        
    }
}
