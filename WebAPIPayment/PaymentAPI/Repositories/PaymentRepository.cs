using PaymentAPI.Domain;
using PaymentAPI.Domain.Entities;
using PaymentAPI.infrastructure.Interfaces;
using PaymentAPI.Repositories.interfaces;
using PaymentAPI.Services.Interface;

namespace PaymentAPI.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IPaymentPIX _payPix;
        private readonly IPaymentCARD _payCard;
        private readonly IPaymentTicket _payTicket;
        private readonly IPublisher _publisher;
        public PaymentRepository(IPublisher publisher, IPaymentPIX payPix, IPaymentCARD payCard, IPaymentTicket payTicket)
        {
            _publisher = publisher;
            _payTicket = payTicket;
            _payCard = payCard;
            _payPix = payPix;
        }

        public ResponseDTO MakePayment(OrderCreateDTO dto)
        {
            var data = new ResponseDTO();

            switch (dto.Method.ToLower())
            {
                case "card":
                    data = _payCard.InsertPayment(dto.Card!);

                    break;

                case "pix":

                    data = _payPix.InsertPayment(dto);
                    break;

                case "ticket":

                    data.Status = "IN PROCESS";

                    break;
                default:

                    break;
            }


            if (data.Error)
            {
                Console.WriteLine("ERROR NA APLICAÇÂO");
                return data;
            }
            dto.Status = data.Status;

            _publisher.Publish(dto, "order-payment");

            return data;
        }
    }
}