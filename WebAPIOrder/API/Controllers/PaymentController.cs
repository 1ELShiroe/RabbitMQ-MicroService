using API.Domain.DTOs;
using API.Domain.Models;
using API.services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("payment")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpPost()]
        public IActionResult GeneratePayment([FromBody] OrderPaymentDTO dto)
        {
            ResponseModel data = _service.GenerateMethod(dto);
            if (data.Error)
            {
                return BadRequest(
                    new
                    {
                        Error = data.Error,
                        Message = data.Message
                    }
                );
            }
            return Ok(
                new
                {
                    Error = data.Error,
                    Message = data.Message
                }
            );
        }
    }
}