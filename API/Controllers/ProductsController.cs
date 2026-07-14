using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext context;
        public ProductsController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await context.products.ToListAsync();
        }

        [HttpGet("{id:int}")] //api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await context.products.FindAsync(id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            context.products.Add(product);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        private bool ProductExists(int id)
        {
            return context.products.Any(p => p.Id == id);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id)) 
                return BadRequest("Cannot update this product");
            context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();            
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await context.products.FindAsync(id);
            if (product == null) return NotFound();
            context.products.Remove(product);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}