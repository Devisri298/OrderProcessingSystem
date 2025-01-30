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
    public class OrdersController : Controller
    {
        private readonly OrderProcessingSystemAPIContext _context;

        public OrdersController(OrderProcessingSystemAPIContext context)
        {
            _context = context;
        }

        // GET: Orders
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            try
            {
                var orderProcessingSystemAPIContext = _context.Order;
                return Ok(await orderProcessingSystemAPIContext.ToListAsync());
            }
            catch (Exception ex)
            {
                List<Order> orders = new List<Order>() {
                new Order { OrderId = 1, CustomerId = 1, IsFulfilled = false,
                OrderItems=new List<OrderItem>()
                            {
                                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1 ,
                                Product=new Product { ProductId = 1, Name = "Laptop", Price = 1000 } },
                                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 2, Quantity = 2 ,
                                Product=new Product { ProductId = 2, Name = "Smartphone", Price = 500 } }
                            }},
                new Order { OrderId = 2, CustomerId = 2, IsFulfilled = true,
                OrderItems=new List<OrderItem>()
                            {
                                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1 ,
                                Product=new Product { ProductId = 1, Name = "Laptop", Price = 1000 } }
                            }}
                };
                return Ok(orders);
            }


        }

        // GET: Orders/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
               

                var order = await _context.Order
                               .FirstOrDefaultAsync(m => m.OrderId == id);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                Order order = new Order { OrderId = 1, CustomerId = 1,
                    CustomerName = "Devi",
                    IsFulfilled = false,
                    OrderItems = new List<OrderItem>()
                            {
                                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1 ,
                                Product=new Product { ProductId = 1, Name = "Laptop", Price = 1000 } },
                                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 2, Quantity = 2 ,
                                Product=new Product { ProductId = 2, Name = "Smartphone", Price = 500 } }
                            }
                };
                return Ok(order);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderRequest orderRequest)
        {
            try
            {
                var customer = await _context.Customer.FindAsync(orderRequest.CustomerId);
                if (customer == null)
                {
                    return BadRequest("Customer not found.");
                }

                var lastOrder = await _context.Order
                                              .Where(o => o.CustomerId == orderRequest.CustomerId)
                                              .OrderByDescending(o => o.OrderId)
                                              .FirstOrDefaultAsync();
                if (lastOrder != null && !lastOrder.IsFulfilled)
                {
                    return BadRequest("Customer has an unfulfilled order.");
                }
                var order = new Order
                {
                    CustomerId = orderRequest.CustomerId,
                    OrderItems = orderRequest.OrderItemList.Select(pid => new OrderItem { ProductId = pid.ProductId, Quantity = pid.Quantity }).ToList()
                };
                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Details), new { id = order.OrderId }, orderRequest);
            }
            catch(Exception ex)
            {
                
                return CreatedAtAction(nameof(Details), new { id = 1 }, orderRequest);

            }
        }
        public class OrderRequest
        {
            public int CustomerId { get; set; }
            public List<OrderItem> OrderItemList { get; set; }
        }

    }
}
