using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Models
{
    public class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required Guid ClientId { get; set; }
        public required Decimal Amount { get; set; }
        public Guid? Payment { get; set; }
        public String Cupom { get; set; } = null!;
        public ICollection<OrderProductModel>? OrderProduct { get; set; }

    }

}
