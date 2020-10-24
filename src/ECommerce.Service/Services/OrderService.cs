using System;
using System.Collections.Generic;
using Ecommerce.Dto;
using ECommerce.Entity;
using ECommerce.UnitOfWork;

namespace ECommerce.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ServiceResponse<Guid> CreateOrder(OrderDto model)
        {
            try
            {
                var product = _uow.ProductRepository.FirstOrDefault(x => x.Code == model.ProductCode);

                var validator = new CreateOrderValidator(_uow, product, model.Quantity);
                var validationResult = validator.Validate(model);

                if (!validationResult.IsValid)
                {
                    return new ServiceResponse<Guid>
                    {
                        Success = false,
                        Message = "Order not created!",
                        Errors = GetValidationErrors(validationResult)
                    };
                }

                var orderPrice = model.Quantity * product.Price;
                var orderData = new Order
                {
                    ProductId = product.Id,
                    Quantity = model.Quantity,
                    OrderDate = DateTime.UtcNow,
                    ItemPrice = product.Price,
                    OrderPrice = orderPrice
                };
                
                _uow.OrderRepository.Add(orderData);
                _uow.SaveChanges();

                return new ServiceResponse<Guid>
                {
                    Success = true,
                    Message = "Order created successfully.",
                    Data = orderData.Id
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Guid>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}