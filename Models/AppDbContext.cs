using Microsoft.EntityFrameworkCore;
using shop.Models;
namespace shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using shop.Configuration.Entities;

public class AppDbContext : IdentityDbContext<ApiUser>
// Inheritance for including roles in query
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        // Form postgres sql date time error
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new RoleConfiguration());

        // builder.Entity<ApiUser>(b =>
        // {
        //     b.HasMany(e => e.UserRoles)
        //     .WithOne(e => e.User)
        //     .HasForeignKey(ur => ur.UserId)
        //     .IsRequired();
        // });

        // builder.Entity<Roles>(b =>
        // {
        //     b.HasMany(e => e.UserRoles)
        //     .WithOne(e => e.Role)
        //     .HasForeignKey(ur => ur.RoleId)
        //     .IsRequired();
        // });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var insertedEntries = this.ChangeTracker.Entries()
                               .Where(x => x.State == EntityState.Added)
                               .Select(x => x.Entity);

        foreach (var insertedEntry in insertedEntries)
        {
            var auditableEntity = insertedEntry as Auditable;
            //If the inserted object is an Auditable. 
            if (auditableEntity != null)
            {
                auditableEntity.DateCreated = DateTimeOffset.UtcNow;
                auditableEntity.DateUpdated = DateTimeOffset.UtcNow;
            }
        }

        var modifiedEntries = this.ChangeTracker.Entries()
                   .Where(x => x.State == EntityState.Modified)
                   .Select(x => x.Entity);

        foreach (var modifiedEntry in modifiedEntries)
        {
            //If the inserted object is an Auditable. 
            var auditableEntity = modifiedEntry as Auditable;
            if (auditableEntity != null)
            {
                auditableEntity.DateUpdated = DateTimeOffset.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<shop.Models.Category> Category { get; set; } = default!;
    public DbSet<shop.Models.Products> Products { get; set; } = default!;
    public DbSet<shop.Models.Roles> Roles { get; set; } = default!;
}