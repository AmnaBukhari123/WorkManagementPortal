// src/Infrastructure/Persistence/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using EnterpriseWorkManagementPortal.Domain.Entities;

namespace EnterpriseWorkManagementPortal.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    private int? _currentUserId;
    public void SetCurrentUser(int userId) => _currentUserId = userId;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        modelBuilder.Entity<TaskItem>().Property(t => t.Status).HasConversion<string>();
        modelBuilder.Entity<TaskItem>().Property(t => t.Priority).HasConversion<string>();

        modelBuilder.Entity<ProjectMember>().HasKey(pm => new { pm.ProjectId, pm.UserId });
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Project>()
            .HasOne(p => p.CreatedBy).WithMany()
            .HasForeignKey(p => p.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectMember>()
            .HasOne(pm => pm.Project).WithMany(p => p.Members)
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProjectMember>()
            .HasOne(pm => pm.User).WithMany(u => u.ProjectMemberships)
            .HasForeignKey(pm => pm.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Project).WithMany(p => p.Tasks)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.AssignedTo).WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.TaskItem).WithMany(t => t.Comments)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Author).WithMany()
            .HasForeignKey(c => c.AuthorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.TaskItem).WithMany(t => t.Attachments)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Attachment>()
            .HasOne(a => a.UploadedBy).WithMany()
            .HasForeignKey(a => a.UploadedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AuditLog>()
            .HasOne(a => a.ChangedBy).WithMany()
            .HasForeignKey(a => a.ChangedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditEntries = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries()
                     .Where(e => e.Entity is not AuditLog &&
                                 (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)))
        {
            auditEntries.Add(new AuditLog
            {
                EntityName = entry.Entity.GetType().Name,
                Action = entry.State.ToString(),
                ChangedByUserId = _currentUserId ?? 0,
                ChangedAt = DateTime.UtcNow
            });
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        if (auditEntries.Any())
        {
            AuditLogs.AddRange(auditEntries);
            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}