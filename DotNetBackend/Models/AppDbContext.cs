using Microsoft.EntityFrameworkCore;
using DotNetBackend.Models;

namespace DotNetBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } // Dodan DbSet za Student klasu
        public DbSet<Professor> Profesors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<InstructionsDate> InstructionsDates { get; set; }
    }
}