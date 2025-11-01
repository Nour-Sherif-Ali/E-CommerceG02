using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ProductModule;
using Microsoft.AspNetCore.Identity;
using Domain.Entities.IdentityModule;

namespace Persistance.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }
            #region Seeding Data
            #region Product Brand
            if (!_dbContext.ProductBrands.Any())
            {
                var BrandsData = File.OpenRead(@"..\Infrastructure\Persistance\Data\DataSeed\brands.json");
                var Brands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);
                if (Brands is not null && Brands.Any())
                {
                    await _dbContext.ProductBrands.AddRangeAsync(Brands); //local

                }
            }
            #endregion
            #region Product Type
            if (!_dbContext.ProductTypes.Any())
            {
                var TypesData = File.OpenRead(@"..\Infrastructure\Persistance\Data\DataSeed\types.json");
                var Types = await JsonSerializer.DeserializeAsync<List<ProductType>>(TypesData);
                if (Types is not null && Types.Any())
                {
                    await _dbContext.ProductTypes.AddRangeAsync(Types); //local

                }
            }
            #endregion
            #region Product
            if (!_dbContext.Products.Any())
            {
                var ProductsData = File.OpenRead(@"..\Infrastructure\Persistance\Data\DataSeed\products.json");
                var Products =await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                   await  _dbContext.Products.AddRangeAsync(Products); //local

                }
            }
            #endregion
           await  _dbContext.SaveChangesAsync();
            #endregion
        }

        public async Task IdentityDataSeed()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Ali",
                        PhoneNumber = "01153715515",
                        UserName = "MohammedAli",
                        EmailConfirmed = true
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Ali",
                        PhoneNumber = "01153715515",
                        UserName = "SalmaAli",
                        EmailConfirmed = true
                    };
                    await _userManager.CreateAsync(User01, "Pa$$w0rd");
                    await _userManager.CreateAsync(User02, "Pa$$w0rd");

                    //Add To Role 
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
