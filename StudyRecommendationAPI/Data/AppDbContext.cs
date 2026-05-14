using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Models;

namespace StudyRecommendationAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<Feedback> Feedbacks => Set<Feedback>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Name).IsRequired();
            e.HasMany(s => s.Topics).WithOne(t => t.Subject).HasForeignKey(t => t.SubjectId);
        });

        modelBuilder.Entity<Topic>(e =>
        {
            e.HasKey(t => t.Id);
            e.HasMany(t => t.Resources).WithOne(r => r.Topic).HasForeignKey(r => r.TopicId);
        });

        modelBuilder.Entity<Resource>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.PositiveVotes).HasDefaultValue(0);
            e.Property(r => r.NegativeVotes).HasDefaultValue(0);
            e.HasMany(r => r.Feedbacks).WithOne(f => f.Resource).HasForeignKey(f => f.ResourceId);
        });

        modelBuilder.Entity<Feedback>(e =>
        {
            e.HasKey(f => f.Id);
            e.HasIndex(f => new { f.ResourceId, f.UserId }).IsUnique();
        });
    }
}
