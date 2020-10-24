using System;
using Ecommerce.Dto;

namespace ECommerce.Service
{
    public interface IOrderService
    {
        ServiceResponse<Guid> CreateOrder(OrderDto model);
    }
}