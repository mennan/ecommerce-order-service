using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dto;
using ECommerce.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Controllers
{
    [ValidateModel]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderModel model)
        {
            var orderData = new OrderDto
            {
                ProductCode = model.ProductCode,
                Quantity = model.Quantity
            };
            var response = _orderService.CreateOrder(orderData);

            return response.Success ? Success(response.Data, response.Message) : BadRequest(response.Errors, response.Message);
        }
    }
}