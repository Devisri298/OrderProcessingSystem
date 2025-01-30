using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderProcessingSystem.Models;
using System.Text.Json.Serialization;

namespace OrderProcessingSystem.Controllers
{
    public class CustomerController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7214/api");
        private readonly HttpClient _client;
        public CustomerController()
        {
            _client = new HttpClient();
            _client.BaseAddress= baseAddress;
        }
        public IActionResult Index()
        {
            List<Customer> customerList = new List<Customer>();
            HttpResponseMessage response=_client.GetAsync(_client.BaseAddress+"/customers").Result;
            if(response.IsSuccessStatusCode)
            {
                string data=response.Content.ReadAsStringAsync().Result;
                customerList=JsonConvert.DeserializeObject<List<Customer>>(data);
            }

            return View(customerList);
        }

        public IActionResult Create()
        {
            Customer customer = new Customer();
            return View(customer);
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index", "Customer");
            }
            return View(customer);
        }

        public IActionResult CustomerOrderDetails(int id)
        {
            Customer customer = new Customer();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/customers/"+id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<Customer>(data);
            }
             return View(customer); 
        }


    }
}
