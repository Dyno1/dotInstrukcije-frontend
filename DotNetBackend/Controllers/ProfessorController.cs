using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotNetBackend.Models;
using DotNetBackend.Data;

namespace DotNetBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jwtSecret;

        public ProfessorController(ApplicationDbContext context)
        {
            _context = context;
            _jwtSecret = "podrucje_za_unos_tajnog_kljuca";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Professor professor)
        {
            if (ModelState.IsValid)
            {
                if (professor.SubjectsCount >= 3)
                {
                    return BadRequest(new { success = false, message = "A professor can instruct a maximum of 3 subjects." });
                }

                _context.Add(professor);
                await _context.SaveChangesAsync();
                return Ok(new { success = true, message = "Professor successfully registered." });
            }
            return BadRequest(new { success = false, message = "Invalid professor data." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Professor professor)
        {
            var existingProfessor = await _context.Professors.FirstOrDefaultAsync(p => p.Email == professor.Email && p.Password == professor.Password);
            if (existingProfessor != null)
            {
                var token = GenerateJwtToken(existingProfessor);
                return Ok(new { success = true, message = "Professor logged in successfully.", token });
            }
            return Unauthorized(new { success = false, message = "Invalid email or password." });
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetProfessorByEmail(string email)
        {
            var professor = await _context.Professors.FirstOrDefaultAsync(p => p.Email == email);
            if (professor == null)
                return NotFound(new { success = false, message = "Professor not found." });
            return Ok(new { success = true, professor });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfessors()
        {
            var professors = await _context.Professors.ToListAsync();
            return Ok(new { success = true, professors });
        }

        private string GenerateJwtToken(Professor professor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, professor.Id.ToString()),
                    new Claim(ClaimTypes.Email, professor.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}