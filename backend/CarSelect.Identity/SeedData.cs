using System.Security.Claims;
using Duende.IdentityModel;
using CarSelect.Identity.Data;
using CarSelect.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;

namespace CarSelect.Identity;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            // Apply migrations
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var configContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            configContext.Database.Migrate();

            // Seed clients and scopes — needed in both dev and production
            Log.Debug("Overwriting db clients with Config.cs");
            configContext.Clients.RemoveRange(configContext.Clients);
            foreach (var client in Config.Clients)
                configContext.Clients.Add(client.ToEntity());
            configContext.SaveChanges();

            Log.Debug("Adding IdentityResources");
            foreach (var resource in Config.IdentityResources)
                if (!configContext.IdentityResources.Any(db => resource.Name == db.Name))
                    configContext.IdentityResources.Add(resource.ToEntity());
            configContext.SaveChanges();

            Log.Debug("Adding ApiScopes");
            foreach (var resource in Config.ApiScopes)
                if (!configContext.ApiScopes.Any(db => resource.Name == db.Name))
                    configContext.ApiScopes.Add(resource.ToEntity());
            configContext.SaveChanges();

            // Seed roles — needed in both dev and production
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleMgr.RoleExistsAsync("Admin").Result)
            {
                roleMgr.CreateAsync(new IdentityRole("Admin")).Wait();
                Log.Debug("Admin role created");
            }

            if (!roleMgr.RoleExistsAsync("User").Result)
            {
                roleMgr.CreateAsync(new IdentityRole("User")).Wait();
                Log.Debug("User role created");
            }

            // Seed test admin user — development only
            if (app.Environment.IsDevelopment())
            {
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var admin = userMgr.FindByNameAsync("admin").Result;
                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@carselect.com",
                        EmailConfirmed = true,
                        FirstName = "Admin",
                        LastName = "User",
                        CreatedAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow
                    };

                    var result = userMgr.CreateAsync(admin, "Admin123$").Result;
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    result = userMgr.AddToRoleAsync(admin, "Admin").Result;
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    result = userMgr.AddClaimsAsync(admin, new Claim[]
                    {
                        new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                        new Claim(JwtClaimTypes.Role, "Admin")
                    }).Result;
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    Log.Debug("Admin user created");
                }
                else
                {
                    Log.Debug("Admin user already exists");
                }
            }
        }
    }
}
