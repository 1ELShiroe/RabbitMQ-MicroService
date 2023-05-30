namespace PaymentAPI.Domain
{
    public class CardModel
    {
        public required String NameCard { get; set; }
        public required String NumberCard { get; set; }
        public required String CVV { get; set; }
        public required String Expiration { get; set; }
    }
}