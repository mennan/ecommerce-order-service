using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Api.Controllers;
using Ecommerce.Dto;
using ECommerce.Service;
using ECommerce.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ECommerce.Api.Tests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _orderService;

        public OrdersControllerTests()
        {
            _orderService = new Mock<IOrderService>();
        }
        
        [Fact]
        public void CreateOrder_GivenValidModel_ReturnSuccess()
        {
            var expectedResult = new ServiceResponse<Guid>
            {
                Success = true,
                Message = "Order created successfully.",
                Data = Guid.NewGuid()
            };
            
            _orderService.Setup(x => x.CreateOrder(It.IsAny<OrderDto>())).Returns(expectedResult);
            
            var controller = new OrdersController(_orderService.Object);
            var data = new OrderModel
            {
                ProductCode = "iphone11",
                Quantity = 3
            };
            var result = controller.CreateOrder(data);
            
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<Guid>>(apiResult.Value);

            Assert.True(model.Success);
            Assert.Equal("Order created successfully.", model.Message);
            Assert.Equal(StatusCodes.Status200OK, apiResult.StatusCode);
            Assert.NotEqual(Guid.Empty, model.Data);
        }
        
        [Fact]
        public void CreateOrder_GivenInvalidModel_ReturnFail()
        {
            var expectedResult = new ServiceResponse<Guid>
            {
                Success = false,
                Message = "Model validation error!"
            };
            
            _orderService.Setup(x => x.CreateOrder(It.IsAny<OrderDto>())).Returns(expectedResult);
            
            var controller = new OrdersController(_orderService.Object);
            var data = new OrderModel
            {
                ProductCode = "iphone11",
                Quantity = 3
            };
            var result = controller.CreateOrder(data);
            
            var apiResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiData<List<string>>>(apiResult.Value);

            Assert.False(model.Success);
            Assert.Equal("Model validation error!", model.Message);
            Assert.Equal(StatusCodes.Status400BadRequest, apiResult.StatusCode);
        }
    }
}