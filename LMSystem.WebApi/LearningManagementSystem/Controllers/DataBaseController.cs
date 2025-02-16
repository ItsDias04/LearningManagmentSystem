using LearningManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using LearningManagementSystem.Models;
using LearningManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DataBaseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataBaseController(IConfiguration configuration, AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("addschool")]
        public IActionResult AddSchool(string NameSchool)
        {
            _context.School.Add(new Schools { Name = NameSchool });
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("addgroup")]
        public IActionResult AddGroup(
            [FromBody] AddGroupDTO group
            ) {
            _context.Group.Add(
                new Group { 
                    Name = group.Name, 
                    School = _context.School.FirstOrDefault(
                        g=> g.Id ==group.SchoolId
                        )}
                );
            _context.SaveChanges();
            return Ok();
            
        }

        [HttpPost("addstudent")]
        public IActionResult AddStudent(
            [FromBody] AddStudentDTO student
            ) {
            _context.Student.Add(new Student
            {
                Email = student.Email,
                UserName = student.Name,
                PasswordHash = student.Password,
                
                Group = _context.Group.FirstOrDefault(
                    g => g.Id == student.GroupId
                )
            }
            );
            _context.SaveChanges();
            return Ok(); 
        }
        [HttpPost("addtutor")]
        public IActionResult AddTutor(
            [FromBody] AddTutorDTO tutor
            )
        {
            _context.Tutor.Add(new Tutor
            {
                Email = tutor.TutorEmail,
                UserName = tutor.TutorName,
                PasswordHash = tutor.TutorPassword,
                
            }
            );
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("addsubjects")]
        public IActionResult AddSubjects([FromBody] AddSubjectsDTO subject)
        {
            _context.Subjects.Add(
                new Subjects { 
                    Name = subject.Name, 
                    Tutor = _context.Tutor.FirstOrDefault(g=> g.Email == subject.TutorEmail)
                });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("addGroupsSubjects")]
        public IActionResult AddGroupSubject(string groupName, string subjectName)
        {
            var group = _context.Group.FirstOrDefault(_g => _g.Name == groupName);
            var subject = _context.Subjects.FirstOrDefault( g => g.Name == subjectName );

            
            group.Subjects.Add(subject);
            
            
            _context.SaveChanges();        
            
            return Ok();
        }
        
    }
}
