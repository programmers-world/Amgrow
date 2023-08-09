namespace aryamoneygrow
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class RoleInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
        {
            // Check if the "User" role already exists
            if (!await roleManager.RoleExistsAsync("User"))
            {
                // Create the "User" role if it doesn't exist
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
            else if (!await roleManager.RoleExistsAsync("Admin"))
            {
                // Create the "User" role if it doesn't exist
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            else if (!await roleManager.RoleExistsAsync("Coin"))
            {
                // Create the "User" role if it doesn't exist
                await roleManager.CreateAsync(new IdentityRole("Coin"));
            }
        }
    }

}
