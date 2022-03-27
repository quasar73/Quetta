using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DataInitializer
    {
        public async static Task Seed(IServiceScope scope)
        { 
            var context = scope.ServiceProvider.GetService<QuettaDbContext>();

            context?.Database?.Migrate();

            await SeedRoles(context);
            context?.SaveChanges();
        }

        private async static Task SeedRoles(QuettaDbContext? context)
        {
            string[] roles = new string[] { "User" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context?.Roles?.Any(r => r.Name == role) ?? false)
                {
                    await roleStore.CreateAsync(new IdentityRole(role)
                    {
                        NormalizedName = role.ToUpper()
                    });
                }
            }
        }
    }
}
