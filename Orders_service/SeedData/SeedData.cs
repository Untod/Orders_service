using Microsoft.EntityFrameworkCore;
using Orders_service.Data;

namespace Orders_service.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider provider)
        {
            using (var context = new Orders_serviceContext(provider.GetRequiredService<DbContextOptions<Orders_serviceContext>>()))
            {
                if (context == null || context.Provider == null)
                    throw new ArgumentNullException("No context found");
                if (context.Provider.Any())
                    return;
                context.AddRange(
                    new Provider { Name = "First provider" },
                    new Provider { Name = "Second provider" },
                    new Provider { Name = "Third provider" }
                    );
                context.SaveChanges();
            }
        }
    }
}
