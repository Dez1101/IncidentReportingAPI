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

        // Seed 55 Units (Blocks A–E, Numbers 101–111)
        modelBuilder.Entity<Unit>().HasData(
            new Unit { Id = 1, Block = "A", Number = "101" },
            new Unit { Id = 2, Block = "A", Number = "102" },
            new Unit { Id = 3, Block = "A", Number = "103" },
            new Unit { Id = 4, Block = "A", Number = "104" },
            new Unit { Id = 5, Block = "A", Number = "105" },
            new Unit { Id = 6, Block = "A", Number = "106" },
            new Unit { Id = 7, Block = "A", Number = "107" },
            new Unit { Id = 8, Block = "A", Number = "108" },
            new Unit { Id = 9, Block = "A", Number = "109" },
            new Unit { Id = 10, Block = "A", Number = "110" },
            new Unit { Id = 11, Block = "A", Number = "111" },

            new Unit { Id = 12, Block = "B", Number = "101" },
            new Unit { Id = 13, Block = "B", Number = "102" },
            new Unit { Id = 14, Block = "B", Number = "103" },
            new Unit { Id = 15, Block = "B", Number = "104" },
            new Unit { Id = 16, Block = "B", Number = "105" },
            new Unit { Id = 17, Block = "B", Number = "106" },
            new Unit { Id = 18, Block = "B", Number = "107" },
            new Unit { Id = 19, Block = "B", Number = "108" },
            new Unit { Id = 20, Block = "B", Number = "109" },
            new Unit { Id = 21, Block = "B", Number = "110" },
            new Unit { Id = 22, Block = "B", Number = "111" },

            new Unit { Id = 23, Block = "C", Number = "101" },
            new Unit { Id = 24, Block = "C", Number = "102" },
            new Unit { Id = 25, Block = "C", Number = "103" },
            new Unit { Id = 26, Block = "C", Number = "104" },
            new Unit { Id = 27, Block = "C", Number = "105" },
            new Unit { Id = 28, Block = "C", Number = "106" },
            new Unit { Id = 29, Block = "C", Number = "107" },
            new Unit { Id = 30, Block = "C", Number = "108" },
            new Unit { Id = 31, Block = "C", Number = "109" },
            new Unit { Id = 32, Block = "C", Number = "110" },
            new Unit { Id = 33, Block = "C", Number = "111" },

            new Unit { Id = 34, Block = "D", Number = "101" },
            new Unit { Id = 35, Block = "D", Number = "102" },
            new Unit { Id = 36, Block = "D", Number = "103" },
            new Unit { Id = 37, Block = "D", Number = "104" },
            new Unit { Id = 38, Block = "D", Number = "105" },
            new Unit { Id = 39, Block = "D", Number = "106" },
            new Unit { Id = 40, Block = "D", Number = "107" },
            new Unit { Id = 41, Block = "D", Number = "108" },
            new Unit { Id = 42, Block = "D", Number = "109" },
            new Unit { Id = 43, Block = "D", Number = "110" },
            new Unit { Id = 44, Block = "D", Number = "111" },

            new Unit { Id = 45, Block = "E", Number = "101" },
            new Unit { Id = 46, Block = "E", Number = "102" },
            new Unit { Id = 47, Block = "E", Number = "103" },
            new Unit { Id = 48, Block = "E", Number = "104" },
            new Unit { Id = 49, Block = "E", Number = "105" },
            new Unit { Id = 50, Block = "E", Number = "106" },
            new Unit { Id = 51, Block = "E", Number = "107" },
            new Unit { Id = 52, Block = "E", Number = "108" },
            new Unit { Id = 53, Block = "E", Number = "109" },
            new Unit { Id = 54, Block = "E", Number = "110" },
            new Unit { Id = 55, Block = "E", Number = "111" }
        );

        // Seed IncidentTypes
        modelBuilder.Entity<IncidentType>().HasData(
            new IncidentType { Id = 1, Name = "Noise" },
            new IncidentType { Id = 2, Name = "Safety" },
            new IncidentType { Id = 3, Name = "Damage" }
        );
    }
}
