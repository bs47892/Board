namespace Board.Models
{
    public class WorkspaceUsers
    {
        public int WorkspaceId { get; set; }
        public int UserId { get; set; }
        public Workspace Workspace { get; set; }
        public User User { get; set; }
    }
}
