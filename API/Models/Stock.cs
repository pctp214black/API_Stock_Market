namespace API.Models;

using System.ComponentModel.DataAnnotations.Schema;

public class Stock
{
    public int Id { get; set; }
    public string Symbol { get; set; } = "";
    public string CompamyName { get; set; } = "";

    [Column(TypeName = "decimal(18,2)")]
    public decimal Purchase { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = "";
    public long MarketCap { get; set; }
    public List<Comment> Comments = new List<Comment>();
}