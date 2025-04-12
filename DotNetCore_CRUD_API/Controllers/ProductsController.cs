using DotNetCore_CRUD_API.Data;
using DotNetCore_CRUD_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly APIDbContext _context;
        public ProductsController(APIDbContext dbContext)
        {
            _context = dbContext;
        }

        //Get all products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.products.ToList();
            return Ok(products);
        }

        //Get product by id
        [HttpGet("products/{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _context.products.FirstOrDefault(x => x.Id == id);
            if (product is null) return NotFound();
            return Ok(product);
        }

        //Add product
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _context.products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        //Update product
        [HttpPut("products/{id:int}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var existProduct = _context.products.FirstOrDefault(x => x.Id == id);
            if (existProduct is null) return NotFound();
            existProduct.Name = product.Name;
            existProduct.Description = product.Description;
            existProduct.Price = product.Price;
            _context.products.Update(existProduct);
            _context.SaveChanges();
            return Ok(existProduct);
        }

        //Delete product
        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.products.FirstOrDefault(x => x.Id == id);
            if (product is null) return NotFound();
            _context.products.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }
    }
}
