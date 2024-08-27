using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resful_project.data;
using resful_project.DTO.Stock;
using resful_project.Helper;
using resful_project.Interfaces;
using resful_project.Mapper;
using resful_project.models;

namespace resful_project.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stocks = await _stockRepository.GetAllStocksAsync(query);
            var stockDto = stocks.Select(x => x.ToStockDTO());
            return Ok(stockDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetStockByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDTO stockdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockmodel = StockMapper.ToStockFromCreateDTO(stockdto);
            await _stockRepository.CreateStockAsync(stockmodel);
            return CreatedAtAction(nameof(GetById), new { id = stockmodel.Id }, stockmodel.ToStockDTO());
        }
        [Route("{id:int}")]
        [HttpPut]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDTO stockdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockmodel = await _stockRepository.UpdateStockAsync(id, stockdto);
            if (stockmodel == null)
            {
                return NotFound();
            }
            return Ok(stockmodel.ToStockDTO());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockmodel = await _stockRepository.DeleteStockByIdAsync(id);
            if (stockmodel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}