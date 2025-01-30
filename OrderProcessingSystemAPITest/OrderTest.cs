using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderProcessingSystemAPI.Controllers;
using OrderProcessingSystemAPI.Data;
using OrderProcessingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderProcessingSystemAPITest
{
    public class OrderTest
    {
        private readonly OrderProcessingSystemAPIContext _mockContext;
        private readonly OrdersController _controller;

        public OrderTest()
        {
            _controller = new OrdersController(_mockContext);
        }

        [Fact]
        public async Task Orders_ReturnsOkResult_WithOrders()
        {
            
            // Act
            var result = await _controller.Orders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrders = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Equal(2, returnOrders.Count);
        }
        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            //_mockContext.Setup(c => c.Order.FirstOrDefaultAsync(m => m.OrderId == orderId))
            //            .ReturnsAsync((Order)null);

            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsOkResult_WithOrder_WhenOrderExists()
        {
            // Arrange
            int orderId = 1;
            //var order = new Order
            //{
            //    OrderId = orderId,
            //    CustomerId = 1,
            //    IsFulfilled = false,
            //    OrderItems = new List<OrderItem>
            //    {
            //        new OrderItem { OrderItemId = 1, OrderId = orderId, ProductId = 1, Quantity = 1 }
            //    }
            //};

            //_mockContext.Setup(c => c.Order.FirstOrDefaultAsync(m => m.OrderId == orderId))
            //            .ReturnsAsync(order);

            // Act
            var result = await _controller.Details(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(orderId, returnOrder.OrderId);
        }

        [Fact]
        public async Task Details_ReturnsOkResult_WhenExceptionOccurs()
        {
            // Arrange
            int orderId = 1;
            //_mockContext.Setup(c => c.Order.FirstOrDefaultAsync(It.IsAny<Func<Order, bool>>()))
            //            .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.Details(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(1, returnOrder.OrderId);  // Default order returned in catch block
        }

    }
}
