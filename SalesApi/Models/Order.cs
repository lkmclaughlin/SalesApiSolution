using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesApi.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    [StringLength(50)]
    public string Description { get; set; } = string.Empty;

    [StringLength(20)]
    public string Status { get; set; } = "NEW";

    [Column(TypeName = "decimal(11,2)")]
    public decimal Total { get; set; } = 0;

    public int? CustomerId { get; set; } = null;

    public virtual Customer? Customer { get; set; } 

    public virtual ICollection<Orderline>? Orderlines { get; set; }
    
}
