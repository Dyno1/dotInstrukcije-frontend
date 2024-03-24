namespace DotNetBackend.Models
{
    public class InstructionSession
    {
        public int StudentId { get; set; }
        public int ProfessorId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }
}