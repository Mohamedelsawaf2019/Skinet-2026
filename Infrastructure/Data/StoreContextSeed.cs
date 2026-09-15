using Core.Entities;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
                if (products == null) return;
                await context.products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}