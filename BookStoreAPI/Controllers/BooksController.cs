using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace BookStoreAPI.Controllers
{
    public class BooksController : ODataController
    {
        private readonly MyDataContext _db;
        public BooksController(MyDataContext db)
        {
            _db = db;
        }
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Books.ToList());
        }
        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return Ok(book);
        }
        [EnableQuery]
        public IActionResult Get(int key)
        {
            return Ok(_db.Books.FirstOrDefault(b => b.Id == key));
        }
        public IActionResult Delete(int key)
        {
            var book = _db.Books.FirstOrDefault(b => b.Id == key);
            if (book is null)
            {
                return NotFound();
            }
            _db.Books.Remove(book);
            _db.SaveChanges();
            return Ok();
        }
        [EnableQuery]
        public IActionResult Put(int key, [FromBody] Book book)
        {
            if (key != book.Id)
            {
                return BadRequest();
            }

            var existingBook = _db.Books.FirstOrDefault(c => c.Id == key);
            if (existingBook == null)
            {
                return NotFound();
            }

            // Update the book properties with the new values
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.Price = book.Price;
            // You can update other properties as needed

            _db.SaveChanges();

            return Updated(book);
        }
    }
}