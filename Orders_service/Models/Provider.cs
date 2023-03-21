using System.ComponentModel.DataAnnotations;

namespace Orders_service.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
