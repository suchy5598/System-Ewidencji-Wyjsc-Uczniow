namespace Projekt.Models
{
    public class Exit
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime ExitTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public string Reason { get; set; }
    }
}
