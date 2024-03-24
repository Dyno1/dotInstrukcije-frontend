using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetBackend.Models;
using DotNetBackend.Data;

namespace DotNetBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                var existingSubject = await _context.Subjects.FirstOrDefaultAsync(s => s.Url == subject.Url);
                if (existingSubject != null)
                    return Conflict(new { success = false, message = "Subject URL already exists." });

                _context.Add(subject);
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Subject created successfully." });
            }
            return BadRequest(new { success = false, message = "Invalid subject data." });
        }

        [HttpGet("{url}")]
        [Authorize]
        public async Task<IActionResult> GetSubjectByUrl(string url)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Url == url);
            if (subject == null)
                return NotFound(new { success = false, message = "Subject not found." });

            var professors = await _context.Professors.Where(p => p.Subjects.Contains(subject.Title)).ToListAsync();

            return Ok(new { success = true, subject, professors });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _context.Subjects.ToListAsync();
            return Ok(new { success = true, subjects });
        }

        [HttpPost("instructions")]
        [Authorize]
        public IActionResult ScheduleInstructionSession(InstructionSession session)
        {
            return Ok(new { success = true, message = "Instruction session scheduled successfully." });
        }
    }
}