using API.DTOs.Stock;
using API.Models;

namespace API.Mappers;

public static class StockMappers
{
    public static StockDto toStockDTO(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompamyName = stockModel.CompamyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap
        };
    }

    public static Stock toStockFromCreateDTO(this CreateStockRequestDto stockDto){
        return new Stock{
            Symbol = stockDto.Symbol,
            CompamyName = stockDto.CompamyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }
}