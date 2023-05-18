using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AutoDependencyInjection.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void RegisterAllEntities<BaseModel>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes()).Where(c => c.IsClass && !c.IsAbstract && c.IsPublic &&
              typeof(BaseModel).IsAssignableFrom(c));
            foreach (Type type in types)
                modelBuilder.Entity(type);
        }
    }
}
