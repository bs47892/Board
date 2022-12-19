using System.ComponentModel.DataAnnotations;


namespace Board.Models
{
    public class List
    {
        [Key]
        public int ListId { get; set; }
        public int WorkspaceId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Workspace Workspace { get; set; }
    }
}
