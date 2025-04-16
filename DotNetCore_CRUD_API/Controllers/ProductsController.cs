using CsvHelper;
using DotNetCore_CRUD_API.Data;
using DotNetCore_CRUD_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace DotNetCore_CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly APIDbContext _context;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(APIDbContext dbContext, ILogger<ProductsController> logger)
        {
            _context = dbContext;
            _logger = logger;
        }

        //Get all products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.products.ToList();
            _logger.LogInformation("Get all products");
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

        //Get product by pagination
        [HttpGet("pagination")]
        public IActionResult GetProduct([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            var products = _context.products.Skip(skip).Take(pageSize).ToList();
            if (products.Count == 0) return NotFound();
            return Ok(products);
        }


        [HttpGet("export-csv")]
        public IActionResult ExportToCsv()
        {
            var products = _context.products.ToList();

            var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true))
            using (var csvWriter = new CsvHelper.CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(products);
            }

            memoryStream.Position = 0; // Reset the position before returning
            return File(memoryStream, "text/csv", "products.csv");
        }


    }
}
