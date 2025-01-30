using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace OrderProcessingSystem.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage="Name is Required")]
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
