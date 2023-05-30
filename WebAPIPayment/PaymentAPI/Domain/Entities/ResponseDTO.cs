namespace PaymentAPI.Domain.Entities
{
    public class ResponseDTO
    {
        public Boolean Error { get; set; }
        public String Message { get; set; } = null!;
        public String Status { get; set; } = null!;
        public CardModel Card { get; set; } = null!;
    }
}