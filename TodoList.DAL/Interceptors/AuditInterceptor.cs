using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;
using TodoList.DAL.Core.Entities;

namespace TodoList.DAL.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly TodoDbContext todoDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var user = string.Empty;
            var usertest = string.Empty;

            if (_httpContextAccessor.HttpContext != null)
            {
                try
                {
                    user = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

                    usertest = _httpContextAccessor.HttpContext.User.Identity.Name;

                }
                catch
                {
                }
            }

            IEnumerable<EntityEntry<BaseEntity>> entries = dbContext.ChangeTracker.Entries<BaseEntity>();

            foreach (EntityEntry<BaseEntity> entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property(e => e.CreateOn).CurrentValue = DateTime.UtcNow;
                    entityEntry.Property(e => e.ModifiedOn).CurrentValue = DateTime.UtcNow;
                }
                if (entityEntry.State == EntityState.Added)
                {
                    //entityEntry.Property(e => e.CreatedById).CurrentValue = user;
                    //entityEntry.Property(e => e.ModifiedById).CurrentValue = user;

                    //entityEntry.Property(e => e.CreatedById).CurrentValue = "b0b24b3d-5741-41cf-a65e-01aecc0fe4ba";
                    //entityEntry.Property(e => e.ModifiedById).CurrentValue = "b0b24b3d-5741-41cf-a65e-01aecc0fe4ba";
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(e => e.ModifiedOn).CurrentValue = DateTime.UtcNow;
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    //entityEntry.Property(e => e.ModifiedById).CurrentValue = user;

                    //entityEntry.Property(e => e.ModifiedById).CurrentValue = "7a987e68-e389-41e0-a5fa-1efdf7827c83";
                }

            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
