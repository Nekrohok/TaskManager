using System.Xml.Linq;

namespace TaskManager.Models
{
    public class TaskModel
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public int Priority { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
