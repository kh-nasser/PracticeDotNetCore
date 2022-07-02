using Microsoft.AspNetCore.Identity;
using token_based_authentication.Data.Helper;

namespace token_based_authentication.Data
{
    public class AppDbInitalizer
    {
        public static async Task SeedRolesToDb(IApplicationBuilder applicationBuilder) {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRole.Manager)) {
                    await roleManager.CreateAsync(new IdentityRole(UserRole.Manager));
                }

                if (!await roleManager.RoleExistsAsync(UserRole.Student))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRole.Student));
                }
            }
        
        }
    }
}
