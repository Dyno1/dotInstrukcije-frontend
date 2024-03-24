using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetBackend.Models
{
    public class Professor
    {
        public int Id { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ProfilePictureUrl { get; set; }
        
        public int InstructionsCount { get; set; }
        
        // Change from array to list
        public List<string> Subjects { get; set; }

        // Add property to track the number of subjects
        public int SubjectsCount => Subjects?.Count ?? 0;
    }
}