using Microsoft.AspNetCore.Identity;

public class DbInitializer
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Admin", "Customer", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Admin
        var admin = new IdentityUser { UserName = "admin@test.com", Email = "admin@test.com" };
        if (await userManager.FindByEmailAsync(admin.Email) == null)
        {
            await userManager.CreateAsync(admin, "Admin@123");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Customer
        var customer = new IdentityUser { UserName = "customer@test.com", Email = "customer@test.com" };
        if (await userManager.FindByEmailAsync(customer.Email) == null)
        {
            await userManager.CreateAsync(customer, "Customer@123");
            await userManager.AddToRoleAsync(customer, "Customer");
        }

        // User
        var user = new IdentityUser { UserName = "user@test.com", Email = "user@test.com" };
        if (await userManager.FindByEmailAsync(user.Email) == null)
        {
            await userManager.CreateAsync(user, "User@123");
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}