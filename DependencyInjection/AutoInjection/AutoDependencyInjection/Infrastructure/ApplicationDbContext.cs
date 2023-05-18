using AutoDependencyInjection.Domain.Common;
using AutoDependencyInjection.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace AutoDependencyInjection.Infrastructure
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            var entitiesAssembly = typeof(BaseModel).Assembly;
            modelBuilder.RegisterAllEntities<BaseModel>(entitiesAssembly);
        }
    }
}
