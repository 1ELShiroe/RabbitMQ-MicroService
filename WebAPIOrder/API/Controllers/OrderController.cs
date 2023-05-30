using API.Domain.DTOs;
using API.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost()]
        // [Authorize]
        public IActionResult Create([FromBody] OrderProductDTO dto)
        {
            var resDTO = _orderService.InsertNewOrder(dto);
            if (resDTO.Error)
            {
                return NotFound(
                    new
                    {
                        Error = true,
                        Message = resDTO.Message
                    }
                );
            }
            return Ok(resDTO);
        }

        [HttpPost("byId")]
        // [Authorize]
        public IActionResult FindById([FromBody] GetGuidDTO dto)
        {
            var order = _orderService.GetOrderById(dto.Id);

            if (order is not null)
            {
                return Ok(new
                {
                    error = true,
                    order = order
                });
            }
            return NotFound(new
            {
                error = true,
                message = "no order found with given id."
            });
        }
        [HttpPost("all")]
        // [Authorize]
        public IActionResult OrderList()
        {
            var order = _orderService.GetAllOrders();
            return Ok(order);
        }

        [HttpPut()]
        // [Authorize]
        public IActionResult FindAndUpdate([FromBody] OrderProductDTO dto)
        {
            var order = _orderService.FindAndUpdateOrder(dto);

            if (order.Error is true)
            {
                return NotFound(new
                {
                    Error = order.Error,
                    Message = order.Message
                });
            }
            return Ok(new
            {
                Error = order.Error,
                Message = "order updated successfully.",
                Order = order.Order
            });
        }
    }
}