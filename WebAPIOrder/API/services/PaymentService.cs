using API.Domain.DTOs;
using API.Domain.Models;
using API.Domain.Validators;
using API.Infrastructure;
using API.Infrastructure.Interface;
using API.services.interfaces;

namespace API.services
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderService _orderService;
        private const String router = "order-create";
        private readonly IPublisher _publisher;
        public PaymentService(IPublisher publisher, IOrderService orderService)
        {
            _orderService = orderService;
            _publisher = publisher;
        }
        private ResponseModel MethodCard(OrderPaymentDTO dto)
        {

            if (dto.Card is not null)
            {
                var validator = new CardValidator().Validate(dto.Card);
                if (validator.IsValid)
                {
                    _publisher.Publish(dto, router);
                    return new ResponseModel()
                    {
                        Error = false,
                        Message = "payment request sent successfully"
                    };
                }
                return new ResponseModel()
                {
                    Error = true,
                    Message = validator.ToString()
                };
            }
            return new ResponseModel()
            {
                Error = true,
                Message = "invalid card information"
            };
        }

        private ResponseModel MethodPIX(OrderPaymentDTO dto)
        {
            _publisher.Publish(dto, router);

            return new ResponseModel()
            {
                Error = false,
                Message = "payment request sent successfully"
            };
        }
        private ResponseModel MethodTicket(OrderPaymentDTO dto)
        {
            _publisher.Publish(dto, router);

            return new ResponseModel()
            {
                Error = false,
                Message = "payment request sent successfully"
            };
        }
        public ResponseModel GenerateMethod(OrderPaymentDTO dto)
        {
            var res = new ResponseModel();
            var order = _orderService.GetOrderById(dto.OrderId);

            if (order is null)
            {
                res.Error = true;
                res.Message = $"the order referenced with ID:{dto.OrderId} for payment was not found";

                return res;
            }

            switch (dto.Method.ToLower())
            {
                case "card":
                    res = this.MethodCard(dto);
                    break;
                case "pix":
                    res = this.MethodPIX(dto);
                    break;
                case "ticket":
                    res = this.MethodTicket(dto);
                    break;
                default:
                    res.Error = true;
                    res.Message = "payment method not found";
                    break;
            }

            return res;
        }
    }
}