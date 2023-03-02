using System.Text.Json.Serialization;

namespace SalesApi.Models;

public class Orderline
{
    public int Id { get; set; }
    public int OrderId { get; set; }

    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;

    public int ItemId { get; set; }
    public virtual Item Item { get; set; } = null!;

    public int Quantity { get; set; } = 1;
}
