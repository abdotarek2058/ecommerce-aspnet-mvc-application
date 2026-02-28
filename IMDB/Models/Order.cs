using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMDB.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public List <OrderItem> OrderItems { get; set; }
        public string PaymentMethod { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
    }
}
