using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders_service.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Номер заказа")]
        public string Number { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2(7)")]
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Поставщик")]
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
        public IList<OrderItem>? Items { get; set; }
    }
}
