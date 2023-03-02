using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace SalesApi.Models;

public class Customer
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(20)]
    public string City { get; set; } = string.Empty;

    [StringLength(2)]
    public string State { get; set; } = string.Empty;

    [Column(TypeName = "decimal(9,2)")]
    public decimal Sales { get; set; } = 0;
    public bool Active { get; set; } = true;

    public Customer() { }
}
