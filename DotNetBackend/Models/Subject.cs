using System.ComponentModel.DataAnnotations;

namespace DotNetBackend.Models
{
    public class Subject
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Url { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}