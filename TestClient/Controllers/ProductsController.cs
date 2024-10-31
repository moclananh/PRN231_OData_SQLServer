
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TestClient.Models;


namespace TestClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient client;
        private string ProductsApiUrl = "";

        public ProductsController()
        {
            client = new HttpClient();
            ProductsApiUrl = "https://localhost:5001/Odata/Products";
            var contentType = "application/json";
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(contentType));

        }

        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync(ProductsApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var data = JsonSerializer.Deserialize<JsonDocument>(strData, options).RootElement;
            var ProductsElement = data.GetProperty("value");

            // Deserialize the "value" property into a list of Product objects.
            var Products = JsonSerializer.Deserialize<IEnumerable<Product>>(ProductsElement.GetRawText(), options);



            return View(Products);
        }

        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Product Product)
        {
            var ProductJson = JsonSerializer.Serialize(Product);
            var content = new StringContent(ProductJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(ProductsApiUrl, content);
            if (!(response.IsSuccessStatusCode))
            {
                return NoContent();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await client.DeleteAsync(ProductsApiUrl + "/" + id.ToString());
            if (!(response.IsSuccessStatusCode))
            {
                return NoContent();
            }
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await client.GetAsync($"{ProductsApiUrl}({id})");

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var Product = JsonSerializer.Deserialize<Product>(strData, options);

                if (Product != null)
                {
                    return View(Product); // Display the edit view with the Product data
                }
            }

            return RedirectToAction("Index"); // Redirect to the index page if the Product is not found or if there's an error
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product Product)
        {
            if (id != Product.Id)
            {
                return BadRequest();
            }

            var ProductJson = JsonSerializer.Serialize(Product);
            var content = new StringContent(ProductJson, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{ProductsApiUrl}({id})", content);

            if (response.IsSuccessStatusCode)
            {
                // Handle success, e.g., show a success message and redirect to the index page
                TempData["Message"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                // Handle the case where the update failed, e.g., display an error message
                TempData["Error"] = "An error occurred while updating the Product";
                return View(Product); // Return the edit view with the Product data to allow the user to correct errors
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            var response = await client.GetAsync($"{ProductsApiUrl}({id})");

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var Product = JsonSerializer.Deserialize<Product>(strData, options);

                if (Product != null)
                {
                    return View(Product); // Display the details view with the Product details
                }
            }

            return RedirectToAction("Index"); // Redirect to the index page if the Product is not found or if there's an error
        }


    }
}
