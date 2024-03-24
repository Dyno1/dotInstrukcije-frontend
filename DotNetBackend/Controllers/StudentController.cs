using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DotNetBackend.Models;
using DotNetBackend.Data;

namespace DotNetBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Student successfully registered." });
            }
            return BadRequest(new { success = false, message = "Invalid student data." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Student student)
        {
            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Email == student.Email && s.Password == student.Password);
            if (existingStudent != null)
            {
                return Ok(new { success = true, message = "Student logged in successfully." });
            }
            return Unauthorized(new { success = false, message = "Invalid email or password." });
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetStudentByEmail(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
            if (student == null)
                return NotFound(new { success = false, message = "Student not found." });
            return Ok(new { success = true, student });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(new { success = true, students });
        }

        [HttpPost("instruction-request")]
        public async Task<IActionResult> RegisterInstructionRequest(InstructionRequest request)
        {
            var existingRequestsCount = await _context.InstructionRequests
                .CountAsync(r => r.StudentId == request.StudentId);

            if (existingRequestsCount >= 3)
            {
                return BadRequest(new { success = false, message = "A student can submit a maximum of 3 instruction requests." });
            }

            _context.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { success = true, message = "Instruction request registered successfully." });
        }
    }
}