namespace backend.Models
{
    public class Group
    {
        // public string Name { get; set; }
        public int Id { get; set; }
        public List<string> Members { get; set; } = new List<string>();
    }
}