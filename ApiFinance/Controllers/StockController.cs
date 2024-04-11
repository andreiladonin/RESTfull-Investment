using ApiFinance.Data;
using ApiFinance.Dtos.Stock;
using ApiFinance.Helpers;
using ApiFinance.Mappers;
using ApiFinance.Models;
using ApiFinance.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

namespace ApiFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _repo;

        public StockController(ApplicationDbContext context, IStockRepository stockRepo) {
            _context = context;
            _repo = stockRepo;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stocks = await _repo.GetStocksAsync(query);
            
            var stockDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _repo.GetStockByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockReuest reuest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Stock stockModel = reuest.ToStockFromCreateDTO();
            var stock = await _repo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut("{id:int}")] 
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequest updateStockRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = await _repo.UpdateAsync(id, updateStockRequest);
            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async  Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockModel = await _repo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
