using LearningManagementSystem.Data;
using LearningManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LearningManagementSystem.DTO;
using System.Text.RegularExpressions;

namespace LearningManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Tutor")]
    public class TutorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TutorController(IConfiguration configuration, AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (username == null)
            {
                return Unauthorized("Username not found in claims.");
            }

            var user = _context.Tutor
                .Include(t => t.Subjects)
                .ThenInclude(s => s.Groups)
                .ThenInclude(g => g.School)
                .FirstOrDefault(t => t.Email == username);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userProfile = new
            {
                user.UserName,
                user.Email,
                user.Bio,
                user.PhoneNumber,
                Subjects = user.Subjects.Select(s => new
                {
                    s.Name,
                    Groups = s.Groups.Select(g => new
                    {
                        g.Name,
                        SchoolName = g.School?.Name ?? "No School Assigned"

                    }),
                    
                })
            };

            return Ok(userProfile);
        }


        [HttpGet("grades")]
        public IActionResult GetGrades(int subjectId, int groupId)
        {
            
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                           ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(username))
                return Unauthorized("Пользователь не авторизован.");

            try
            {
                
                var grades = _context.Grades
                    .Where(g => g.Subjects.Id == subjectId &&
                                g.Student.Group.Id == groupId &&
                                g.Subjects.Tutor.Email == username)
                    .Select(g => new
                    {
                        TutorUserName = g.Subjects.Tutor.UserName,
                        StudentId = g.Student.Id,
                        StudentUserName = g.Student.UserName,
                        GroupName = g.Student.Group.Name,
                        SubjectName = g.Subjects.Name,
                        GradeNumber = g.gradenum
                    })
                    .ToList();

                if (!grades.Any())
                    return NotFound("Оценки не найдены для заданного предмета и группы.");

                return Ok(grades);
            }
            catch (Exception ex)
            {
                
                Console.Error.WriteLine($"Ошибка при получении оценок: {ex.Message}");
                return StatusCode(500, "Произошла ошибка при обработке запроса.");
            }
        }


        [HttpGet("getgroups")]
        public IActionResult GetGroups()
        {
            
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                           ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(username))
                return Unauthorized("Пользователь не авторизован.");

            try
            {
                
                var groups = _context.Tutor
                    .Where(t => t.Email == username)
                    .Include(t => t.Subjects)
                    .ThenInclude(s => s.Groups)
                    .SelectMany(t => t.Subjects.SelectMany(s => s.Groups))
                    .Distinct() 
                    .Select(g => new
                    {
                        g.Id,
                        g.Name
                    })
                    .ToList();

                if (!groups.Any())
                    return NotFound("Группы не найдены для данного преподавателя.");

                return Ok(groups);
            }
            catch (Exception ex)
            {
                
                Console.Error.WriteLine($"Ошибка при получении групп: {ex.Message}");
                return StatusCode(500, "Произошла ошибка при обработке запроса.");
            }
        }


        [HttpGet("getsubjects")]
        public IActionResult GetSubjects()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
               ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var tutor = _context.Tutor
            .Include(t => t.Subjects)
            .ThenInclude(g=> g.Groups)
            .ThenInclude(s=> s.Students)
            .FirstOrDefault(t => t.Email == username);

            if (tutor == null)
            {
                return NotFound("Tutor not found.");
            }

            var subjects = tutor.Subjects.Select(g => new { 
                g.Id, 
                g.Name,
                g.Description,
                GroupsCount = g.Groups.Count,
                StudentsCount = g.Groups.Sum(g => g.Students.Count),
            });

            return Ok(subjects);
        }

        [HttpPost("addgrades")]
        public IActionResult AddGrades([FromBody] List<AddGradeDTO> grades)
        {
            
            var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                           ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (username == null)
                return Unauthorized();

            
            var tutor = _context.Tutor.Include(t => t.Subjects).FirstOrDefault(t => t.Email == username);
            if (tutor == null || tutor.Subjects == null || !tutor.Subjects.Any())
                return BadRequest("Преподаватель не найден или не имеет привязанных предметов.");

            var tutorSubjects = tutor.Subjects.Select(s => s.Id).ToHashSet();

            
            foreach (var grade in grades)
            {
                var student = _context.Student.Include(s => s.Group).ThenInclude(g => g.Subjects)
                                 .FirstOrDefault(s => s.Id == grade.StudentId);

                if (student == null || student.Group?.Subjects == null)
                    return BadRequest($"Студент с ID {grade.StudentId} не найден или не имеет привязанных предметов.");

                var studentSubjects = student.Group.Subjects.Select(s => s.Id).ToHashSet();

                if (!tutorSubjects.Overlaps(studentSubjects))
                    return BadRequest($"Предмет не совпадает для студента ID {grade.StudentId}.");

                
                _context.Grades.Add(new Grades
                {
                    gradenum = grade.Grade,
                    StudentId = grade.StudentId,
                    SubjectsId = grade.SubjectId
                });
            }

            _context.SaveChanges();

            return Ok();
        
            
        }
        [HttpGet("studentsGradesByGroup")]
        public IActionResult GetStudentsGradesByGroup([FromQuery] int GroupId, [FromQuery] int SubjectId)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            var group = _context.Tutor
        .Where(t => t.Email == email)
        .SelectMany(t => t.Subjects)
        .Where(s => s.Id == SubjectId)
        .SelectMany(s => s.Groups)
        .Include(g => g.Students)
        .ThenInclude(s => s.Grades)
        .FirstOrDefault(g => g.Id == GroupId);

            if (group == null)
            {
                return NotFound("Group not found or you don't have access to it.");
            }

            var students = group.Students.Select(s => new
            {
                s.Id,
                s.UserName,
                Grades = s.Grades
                    .Where(g => g.SubjectsId == SubjectId)
                    .Select(g => new { GradeNumber = g.gradenum })
            });

            return Ok(students);
        }

        [HttpGet("getsubjectsgroups")]
        public IActionResult GetSubjectsAndGroups()
        {
            
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            var subjectsGroups = _context.Tutor
                .Include(t=>t.Subjects)
                .ThenInclude(s=>s.Groups)
                .FirstOrDefault(t => t.Email == email)
                .Subjects.Select( s=> new {
                    s.Id,

                    s.Name,
                    Groups = s.Groups.Select(g => new { 
                        g.Id,
                        g.Name
                    })
                });

            return Ok(subjectsGroups);
        }

        [HttpPost("students/filter")]
        public IActionResult GetStudentsByFilter([FromBody] GetStudentsFilterDTO filterParams)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;


            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }

            try
            {
                
                var allStudents = _context.Student
                    .Include(s => s.Group)
                    .Include(s => s.Group.Subjects)
                    .AsQueryable();

                
                var filteredByTutor = allStudents
                    
                    .Where(s => s.Group.Subjects.Any(g => g.Tutor.Email== email));

                var filteredByUserName = string.IsNullOrEmpty(filterParams.FilterByUserName)
                    ? filteredByTutor
                    : filteredByTutor.Where(s => s.UserName.ToLower().Contains(filterParams.FilterByUserName.ToLower()));

                
                var filteredByGroupName = filterParams.FilterByGroupName.Count == 0
                    ? filteredByUserName
                    : filteredByUserName.Where(s => filterParams.FilterByGroupName.Any(g => s.Group.Name == g));

                
                var filteredBySubjectName = filterParams.FilterBySubjectName.Count == 0
                    ? filteredByGroupName
                    : filteredByGroupName.Where(s => filterParams.FilterBySubjectName.Any(g => s.Group.Subjects.Any(j => j.Name == g)));

                
                
                var students = filteredBySubjectName
                    .Select(s => new
                    {
                        Email = s.Email,
                        UserName = s.UserName,
                        GroupName = s.Group.Name,
                        
                        Bio = s.Bio,
                        PhoneNumber = s.PhoneNumber
                    })
                    .ToList();

                return Ok(students);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        

    }
}
