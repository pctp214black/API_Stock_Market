using API.Data;
using API.DTOs.Stock;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public StockController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        /*
        Esto es una forma de hacerlo sin la necesidad de un Mapper y un metodo de extensión
        var stocks = _context.Stocks.ToList()
            .Select(s =>
            {
                return new StockDto
                {
                    Id = s.Id,
                    Symbol = s.Symbol,
                    CompamyName = s.CompamyName,
                    Purchase = s.Purchase,
                    LastDiv = s.LastDiv,
                    Industry = s.Industry,
                    MarketCap = s.MarketCap
                };
            });*/
        var stocks = await _context.Stocks.ToListAsync();
        var stockDto=stocks.Select(s => s.toStockDTO());
        return Ok(stocks);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock =await _context.Stocks.FindAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.toStockDTO());
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel=stockDto.toStockFromCreateDTO();
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById),new {id=stockModel.Id},stockModel.toStockDTO());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto){
        var stockModel=await _context.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
        if(stockModel==null){
            return NotFound();
        }
        stockModel.Symbol = updateDto.Symbol;
        stockModel.CompamyName = updateDto.CompamyName;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.LastDiv = updateDto.LastDiv;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;
        await _context.SaveChangesAsync();
        return Ok(stockModel.toStockDTO());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var stockModel=await _context.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
        if (stockModel==null){
            return NotFound();  
        }
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}