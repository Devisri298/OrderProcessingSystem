using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderProcessingSystemAPI.Controllers;
using OrderProcessingSystemAPI.Data;
using OrderProcessingSystemAPI.Models;
using Xunit;

namespace OrderProcessingSystemAPITest
{
    public class CustomerTest
    {
        private readonly OrderProcessingSystemAPIContext _mockContext;
        private readonly CustomersController _controller;

        public CustomerTest()
        {
            // Mocking the DbSet and the context
            var data = new List<Customer>
        {
            new Customer { CustomerId = 1, Name = "Devisri", Orders = new List<Order>() }
        }.AsQueryable();


            // _mockContext = new Mock<OrderProcessingSystemAPIContext>();
            //_mockContext.Setup(c => c.Customer).Returns(mockDbSet.Object);
            _controller = new CustomersController(_mockContext);
            //_controller = new CustomersController(_mockContext.Object);
        }

        [Fact]
        public async Task Index_ReturnsOkResult_WithCustomerList()
        {
            // Act
            
            var result = await _controller.Index();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<Customer>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task Index_ReturnsOkResult_WithMockCustomerData()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<Customer>>(okResult.Value);
            Assert.Single(returnValue);
            Assert.Equal("Devisri", returnValue.First().Name);
        }
        [Fact]
        public async Task Details_ReturnsNotFound_WhenCustomerIdIsNull()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
          //  _mockContext.Setup(c => c.Customer.FirstOrDefaultAsync(It.IsAny<CancellationToken>()))
                    //    .ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsOkResult_WithCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "Devisri" };
           // _mockContext.Setup(c => c.Customer.FirstOrDefaultAsync(It.IsAny<CancellationToken>()))
                     //   .ReturnsAsync(customer);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal("Devisri", returnValue.Name);
        }

    }
}

