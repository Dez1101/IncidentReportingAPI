using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IncidentReportingApi.Data;

public class IRAuthDbContext : IdentityDbContext
{
    public IRAuthDbContext(DbContextOptions<IRAuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var tenantRoleId = "f1a72ab0-2d7d-4c63-8f65-c2c65d0f1123";
        var adminRoleId = "e3b18d91-39f4-48a9-90db-f87ffbc267d9";

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = tenantRoleId,
                Name = "Tenant",
                NormalizedName = "TENANT",
                ConcurrencyStamp = tenantRoleId
            },
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = adminRoleId
            }
        );
    }
}
