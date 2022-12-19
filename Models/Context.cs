using Microsoft.EntityFrameworkCore;

namespace Board.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardUser> CardUsers { get; set; }
        public DbSet<WorkspaceUsers> WorkspaceUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkspaceUsers>()
                .HasKey(bc => new { bc.WorkspaceId, bc.UserId });
            modelBuilder.Entity<WorkspaceUsers>()
                .HasOne(bc => bc.Workspace)
                .WithMany(b => b.WorkspaceUsers)
                .HasForeignKey(bc => bc.WorkspaceId);
            modelBuilder.Entity<WorkspaceUsers>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.WorkspaceUsers)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<CardUser>()
             .HasKey(bc => new { bc.CardId, bc.UserId });
            modelBuilder.Entity<CardUser>()
                .HasOne(bc => bc.Card)
                .WithMany(b => b.CardUsers)
                .HasForeignKey(bc => bc.CardId);
            modelBuilder.Entity<CardUser>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.CardUsers)
                .HasForeignKey(bc => bc.UserId);
        }
    }

}
