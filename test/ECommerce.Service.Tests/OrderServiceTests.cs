using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ecommerce.Dto;
using ECommerce.Entity;
using Ecommerce.Repository;
using ECommerce.UnitOfWork;
using Moq;
using Xunit;

namespace ECommerce.Service.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void CreateOrder_GivenInvalidModel_ReturnValidationMessages()
        {
            var orderRepository = new Mock<IRepository<Order>>();
            var productRepository = new Mock<IRepository<Product>>();
            orderRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns((Expression<Func<Order, bool>> predicate) => null);
            productRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns((Expression<Func<Product, bool>> predicate) => null);

            var uow = new Mock<IUnitOfWork>();
            uow.Setup(x => x.OrderRepository).Returns(orderRepository.Object);
            uow.Setup(x => x.ProductRepository).Returns(productRepository.Object);

            var orderService = new OrderService(uow.Object);
            var orderData = new OrderDto();
            var result = orderService.CreateOrder(orderData);

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotNull(result.Errors);
            Assert.Equal("Order not created!", result.Message);
        }

        [Fact]
        public void CreateOrder_GivenValidModel_ReturnSuccess()
        {
            var products = new Product
            {
                Id = Guid.NewGuid(),
                Code = "iphone",
                Name = "iPhone 11",
                Price = 1000,
                Stock = 10
            };
            var orderRepository = new Mock<IRepository<Order>>();
            var productRepository = new Mock<IRepository<Product>>();
            orderRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns((Expression<Func<Order, bool>> predicate) => new List<Order>().AsQueryable());
            productRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns((Expression<Func<Product, bool>> predicate) => products);

            var uow = new Mock<IUnitOfWork>();
            uow.Setup(x => x.OrderRepository).Returns(orderRepository.Object);
            uow.Setup(x => x.ProductRepository).Returns(productRepository.Object);

            var orderService = new OrderService(uow.Object);
            var orderData = new OrderDto {ProductCode = "iphone", Quantity = 10};
            var result = orderService.CreateOrder(orderData);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Null(result.Errors);
            Assert.Equal("Order created successfully.", result.Message);
        }

        [Fact]
        public void CreateOrder_GivenOutOfStockProduct_ReturnValidationMessages()
        {
            var products = new Product
            {
                Id = Guid.NewGuid(),
                Code = "iphone",
                Name = "iPhone 11",
                Price = 1000,
                Stock = 100
            };
            var orderRepository = new Mock<IRepository<Order>>();
            var productRepository = new Mock<IRepository<Product>>();
            orderRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns((Expression<Func<Order, bool>> predicate) => new List<Order>().AsQueryable());
            productRepository.Setup(x => x.FirstOrDefault(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns((Expression<Func<Product, bool>> predicate) => products);

            var uow = new Mock<IUnitOfWork>();
            uow.Setup(x => x.OrderRepository).Returns(orderRepository.Object);
            uow.Setup(x => x.ProductRepository).Returns(productRepository.Object);

            var orderService = new OrderService(uow.Object);
            var orderData = new OrderDto {ProductCode = "iphone", Quantity = 101};
            var result = orderService.CreateOrder(orderData);

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.NotNull(result.Errors);
            Assert.Collection(result.Errors, x => Assert.Contains("Product stock is insufficient!", x));
            Assert.Equal("Order not created!", result.Message);
        }
    }
}