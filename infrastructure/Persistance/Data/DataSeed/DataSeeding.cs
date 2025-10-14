using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
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
    }
}
