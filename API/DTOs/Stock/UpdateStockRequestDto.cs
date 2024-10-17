namespace API.DTOs.Stock;

public class UpdateStockRequestDto
{
    public string Symbol { get; set; } = "";
    public string CompamyName { get; set; } = "";
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = "";
    public long MarketCap { get; set; }
}