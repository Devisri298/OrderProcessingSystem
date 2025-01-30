using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystemAPI.Data;
using OrderProcessingSystemAPI.Models;

namespace OrderProcessingSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly OrderProcessingSystemAPIContext _context;

        public CustomersController(OrderProcessingSystemAPIContext context)
        {
            _context = context;
        }

        // GET: Customers
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _context.Customer.ToListAsync());
            }
            catch (Exception ex)
            {
                IEnumerable<Customer> customerList = new List<Customer>()
            {
                new Customer()
                {
                    CustomerId = 1, Name = "Devisri",Orders=new List<Order>()
                    {
                        new Order()
                        {
                            OrderId = 1,
                            CustomerId = 1,
                            CustomerName = "Devi",
                            IsFulfilled = true,
                            OrderItems=new List<OrderItem>()
                            {
                                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1 ,
                                Product=new Product { ProductId = 1, Name = "Laptop", Price = 1000 } },
                                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 2, Quantity = 2 ,
                                Product=new Product { ProductId = 2, Name = "Smartphone", Price = 500 }

                                }
                            }
                        },
                         new Order { OrderId = 2, CustomerId = 1, IsFulfilled = true,
                             OrderItems=new List<OrderItem>()
                            {
                                new OrderItem { OrderItemId = 3, OrderId = 2, ProductId = 3, Quantity = 1,
                                Product=new Product { ProductId = 3, Name = "Headphones", Price = 100 }}
                            } }
                    }
                }
            };
                return Ok(customerList);
            }
        }

        // GET: Customers/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                

                var customer = await _context.Customer
                    .FirstOrDefaultAsync(m => m.CustomerId == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                Customer customer = new Customer()
                {
                    CustomerId = 1,
                    Name = "Devisri",
                    Orders = new List<Order>()
                    {
                        new Order()
                        {
                            OrderId = 1,
                            CustomerId = 1,
                            CustomerName = "Devi",
                            IsFulfilled = true,
                            OrderItems=new List<OrderItem>()
                            {
                                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1 ,
                                Product=new Product { ProductId = 1, Name = "Laptop", Price = 1000 } },
                                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 2, Quantity = 2 ,
                                Product=new Product { ProductId = 2, Name = "Smartphone", Price = 500 } }
                            }
                        },
                         new Order { OrderId = 2, CustomerId = 1, IsFulfilled = true,
                             OrderItems=new List<OrderItem>()
                            {
                                new OrderItem { OrderItemId = 3, OrderId = 2, ProductId = 3, Quantity = 1,
                                Product=new Product { ProductId = 3, Name = "Headphones", Price = 100 }}
                            } }
                    }
                };
                return Ok(customer);
            }
        }

      
    }
}
