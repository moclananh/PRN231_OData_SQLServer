using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AppClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient client;
        private string BooksApiUrl = "";

        public BooksController()
        {
            client = new HttpClient();
            BooksApiUrl = "https://localhost:5001/Odata/Books";
            var contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(contentType));

        }

        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync(BooksApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = JsonSerializer.Deserialize<JsonDocument>(strData, options).RootElement;
            var booksElement = data.GetProperty("value");

            // Deserialize the "value" property into a list of Book objects.
            var books = JsonSerializer.Deserialize<IEnumerable<Book>>(booksElement.GetRawText(), options);



            return View(books);
        }

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBook book)
        {
            var bookJson = JsonSerializer.Serialize(book);
            var content = new StringContent(bookJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(BooksApiUrl, content);
            if (!(response.IsSuccessStatusCode))
            {
                return NoContent();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await client.DeleteAsync(BooksApiUrl + "/" + id.ToString());
            if (!(response.IsSuccessStatusCode))
            {
                return NoContent();
            }
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await client.GetAsync($"{BooksApiUrl}({id})");

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var book = JsonSerializer.Deserialize<Book>(strData, options);

                if (book != null)
                {
                    return View(book); // Display the edit view with the book data
                }
            }

            return RedirectToAction("Index"); // Redirect to the index page if the book is not found or if there's an error
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            var bookJson = JsonSerializer.Serialize(book);
            var content = new StringContent(bookJson, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{BooksApiUrl}({id})", content);

            if (response.IsSuccessStatusCode)
            {
                // Handle success, e.g., show a success message and redirect to the index page
                TempData["Message"] = "Book updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the case where the update failed, e.g., display an error message
                TempData["Error"] = "An error occurred while updating the book";
                return View(book); // Return the edit view with the book data to allow the user to correct errors
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await client.GetAsync($"{BooksApiUrl}({id})");

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var book = JsonSerializer.Deserialize<Book>(strData, options);

                if (book != null)
                {
                    return View(book); // Display the details view with the book details
                }
            }

            return RedirectToAction("Index"); // Redirect to the index page if the book is not found or if there's an error
        }


    }
}