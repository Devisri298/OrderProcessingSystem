using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderProcessingSystem.Models;
using System.Text;

namespace OrderProcessingSystem.Controllers
{
    public class OrderController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7214/api");
        private readonly HttpClient _client;
        public OrderController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<Product> productList = new List<Product>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/product").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<Product>>(data);
            }
            return View(productList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string OrderData)
        {
             OrderRequest order = new OrderRequest();
            HttpResponseMessage response = new HttpResponseMessage();
            if (!string.IsNullOrEmpty(OrderData))
            {
                var items = JsonConvert.DeserializeObject<List<OrderItem>>(OrderData);
               
                order.CustomerId = 1;
                order.OrderItemList = items;
                var stringPayload = JsonConvert.SerializeObject(order);
                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");


                if (ModelState.IsValid)
                {
                    response = await _client.PostAsync(_client.BaseAddress + "/Orders", httpContent);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = response.Content.ReadAsStringAsync().Result;
                        order = JsonConvert.DeserializeObject<OrderRequest>(data);
                    }

                    return RedirectToAction("Index", "Customer"); // Redirect after successful creation
                }
            }
            
                List<Product> productList = new List<Product>();
                response = _client.GetAsync(_client.BaseAddress + "/product").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    productList = JsonConvert.DeserializeObject<List<Product>>(data);
                }
                return View(productList);

           

         
          
        }

    }
}
