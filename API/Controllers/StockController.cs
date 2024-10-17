using API.Data;
using API.DTOs.Stock;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IStockRepository _stockRepo;
    public StockController(ApplicationDBContext context,IStockRepository stockRepository)
    {
        _stockRepo=stockRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepo.GetAllAsync();
        var stockDto=stocks.Select(s => s.toStockDTO());
        return Ok(stocks);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var stock =await _stockRepo.GetByIdAsync(id);
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
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById),new {id=stockModel.Id},stockModel.toStockDTO());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto){
        var stockModel=await _stockRepo.UpdateAsync(id,updateDto);
        if(stockModel==null){
            return NotFound();
        }
        return Ok(stockModel.toStockDTO());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var stockModel=await _stockRepo.DeleteAsync(id);
        if (stockModel==null){
            return NotFound();  
        }
        return NoContent();
    }
}