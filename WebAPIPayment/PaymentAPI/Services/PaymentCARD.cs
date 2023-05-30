using PaymentAPI.Domain;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Domain.Validators;
using PaymentAPI.Services.Interface;

namespace PaymentAPI.Services
{
    public class PaymentCARD : IPaymentCARD
    {
        public PaymentCARD() { }

        public ResponseDTO InsertPayment(CardModel card)
        {
            var res = new ResponseDTO();
            try
            {
                var validator = new CardModelValidator().Validate(card);

                if (!validator.IsValid)
                {
                    res.Error = true;
                    res.Message = validator.ToString();
                    return res;
                }

                res.Error = false;
                res.Status = "PAID";
                res.Card = card;

                return res;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                res.Error = true;
                res.Message = "no card found within the request";

                return res;
            }
        }
    }
}