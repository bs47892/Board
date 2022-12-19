using System.ComponentModel.DataAnnotations;

namespace Board.Models
{
    public class Workspace
    {
        [Key]
        public int WorkspaceId { get; set; }
        public string Name { get; set; }
        public List<WorkspaceUsers> WorkspaceUsers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
