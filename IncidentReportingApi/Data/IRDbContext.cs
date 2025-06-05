using IncidentReportingApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class IRDbContext : DbContext
{
    public IRDbContext(DbContextOptions<IRDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<IncidentType> IncidentTypes => Set<IncidentType>();
    public DbSet<IncidentMedia> IncidentMedia => Set<IncidentMedia>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enum to string mapping
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<Incident>()
            .Property(i => i.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Incident>()
            .Property(i => i.Priority)
            .HasConversion<string>();

        // Relationships
        modelBuilder.Entity<User>()
            .HasOne(u => u.Unit)
            .WithMany(u => u.Users)
            .HasForeignKey(u => u.UnitId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Incident>()
            .HasOne(i => i.User)
            .WithMany(u => u.Incidents)
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<Incident>()
            .HasOne(i => i.IncidentType)
            .WithMany(it => it.Incidents)
            .HasForeignKey(i => i.IncidentTypeId);

        modelBuilder.Entity<IncidentMedia>()
            .HasOne(m => m.Incident)
            .WithMany(i => i.Media)
            .HasForeignKey(m => m.IncidentId);
    }
}
