using BlogProjectMVC.Data;
using BlogProjectMVC.Enums;
using BlogProjectMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProjectMVC.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext,
                           RoleManager<IdentityRole> roleManager,
                           UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            // Create DB from the Migrations
            await _dbContext.Database.MigrateAsync();

            // 1) Seed roles into the system
            await SeedRolesAsync();

            // 2) Seed users into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            // If there are already roles in the system, do nothing
            if (_dbContext.Roles.Any()) return;

            // Otherwise, create roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                // Use role manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            // If there are already users in the system, do nothing
            if (_dbContext.Users.Any()) return;

            // Admin user
            // 1) Create new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "thejimrogers@pm.me",
                UserName = "thejimrogers@pm.me",
                FirstName = "Jim",
                LastName = "Rogers",
                PhoneNumber = "(331) 684-7740",
                EmailConfirmed = true
            };

            // 2) Use the UserManager to create new user defined by adminUser variable
            await _userManager.CreateAsync(adminUser, "Showpiece#Steadier21");

            // 3) Add this new user to administrator role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            // Moderator user
            var modUser = new BlogUser()
            {
                Email = "jimrogers3315@gmail.com",
                UserName = "jimrogers3315@gmail.com",
                FirstName = "James",
                LastName = "Rogers",
                PhoneNumber = "(630) 946-6643",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(modUser, "Showpiece#Steadier21");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }
    }
}