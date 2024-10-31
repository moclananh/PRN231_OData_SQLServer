
using DemoOData.Configuations;
using DemoOData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;


namespace DemoOData.Controllers
{
    public class ProductsController : ODataController
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db)
        {
            _db = db;
        }
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Products.ToList());
        }
        [EnableQuery]
        public IActionResult Post([FromBody] Product Product)
        {
            _db.Products.Add(Product);
            _db.SaveChanges();
            return Ok(Product);
        }
        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_db.Products.FirstOrDefault(b => b.Id == key));
        }
        public IActionResult Delete(int key)
        {
            var Product = _db.Products.FirstOrDefault(b => b.Id == key);
            if (Product is null)
            {
                return NotFound();
            }
            _db.Products.Remove(Product);
            _db.SaveChanges();
            return Ok();
        }
        [EnableQuery]
        public IActionResult Put(int key, [FromBody] Product Product)
        {
            if (key != Product.Id)
            {
                return BadRequest();
            }

            var existingProduct = _db.Products.FirstOrDefault(c => c.Id == key);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the Product properties with the new values
            existingProduct.Name = Product.Name;
            existingProduct.Description = Product.Description;
            existingProduct.Price = Product.Price;
            existingProduct.CategoryId = Product.CategoryId;
            // You can update other properties as needed

            _db.SaveChanges();

            return Updated(Product);
        }
    }
}

