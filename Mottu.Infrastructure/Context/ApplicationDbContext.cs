using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Desafio.Domain.Entities;
using Desafio.Domain.Common;

namespace Desafio.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<ClientEntity> ClientEntity { get; set; }
    public DbSet<PreRegistrationEntity> PreRegistrationEntity { get; set; }
    public DbSet<PreUpdateEmailEntity> PreUpdateEmailEntity { get; set; }
    public DbSet<RefreshTokenEntity> RefreshTokenEntity { get; set; }
    public DbSet<ServiceEntity> ServiceEntity { get; set; }
    public DbSet<UserEntity> UserEntity { get; set; }
    public DbSet<PlanEntity> PlanEntity { get; set; }
    public DbSet<MotorcycleEntity> MotorcycleEntity { get; set; }
    public DbSet<UserTypeEntity> UserTypeEntity { get; set; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == "id")?.Value;
                    entry.Entity.Created = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == "id")?.Value;
                    entry.Entity.LastModified = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
