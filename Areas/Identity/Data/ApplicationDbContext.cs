using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Board.Models;

namespace Board.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public DbSet<List> Lists { get; set; }
    public DbSet<Card> Cards { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        builder.Entity<List>()
            .HasOne(b => b.CreatedBy)
            .WithMany()
            .HasForeignKey(b => b.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<List>()
            .HasOne(b => b.UpdatedBy)
            .WithMany()
            .HasForeignKey(b => b.UpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.Lists)
            .WithOne(l => l.CreatedBy)
            .HasForeignKey(l => l.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Entity<Card>()
            .HasOne(b => b.CreatedBy)
            .WithMany()
            .HasForeignKey(b => b.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Card>()
            .HasOne(b => b.UpdatedBy)
            .WithMany()
            .HasForeignKey(b => b.UpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Card>()
            .HasOne(c => c.List)
            .WithMany(l => l.Cards)
            .HasForeignKey(c => c.ListId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
}
