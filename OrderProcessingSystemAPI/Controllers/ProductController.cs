using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystemAPI.Data;
using OrderProcessingSystemAPI.Models;

namespace OrderProcessingSystemAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly OrderProcessingSystemAPIContext _context;

        public ProductController(OrderProcessingSystemAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _context.Product.ToListAsync());
            }
            catch (Exception ex)
            {
                IEnumerable<Product> productList = new List<Product>()
            {
               new Product { ProductId = 1, Name = "Laptop", Price = 1000 } ,
                new Product { ProductId = 2, Name = "Smartphone", Price = 500 },
                    new Product { ProductId = 3, Name = "Headphones", Price = 100 }
                            };

                return Ok(productList);
            }
        }
    }
}
