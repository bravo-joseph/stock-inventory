using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using resful_project.DTO.Response;
using resful_project.Extensions;
using resful_project.Interfaces;
using System.Net;
using resful_project.models;

namespace resful_project.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IStockRepository _stockRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }
        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetUserPortfolios([FromRoute] string id)
        {
            Response response = new();
            var username = User.GetUsername();
            var email = User.GetEmail();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return NotFound();
            var userportfolio = await _portfolioRepository.GetUserPortfolios(appUser);
            response.Result = userportfolio;
            response.status = HttpStatusCode.OK;
            return Ok(response);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUserPortfolio(string symbol)
        {
            Response response = new();
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null) return NotFound("stock not found");
            var userportfolio = await _portfolioRepository.GetUserPortfolios(appUser);
            if (userportfolio.Any(p => p.Symbol.ToLower().Equals(symbol.ToLower()))) return BadRequest("Cannot Add Duplicate Stock");

            var portfoliomodel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };
            await _portfolioRepository.AddPortfolioAsync(portfoliomodel);
            response.status = HttpStatusCode.OK;
            response.Result = Results.Ok("Portfolio Added Successfully");
            return Ok(response);
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            string username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            IEnumerable<Stock> userPortfolios = await _portfolioRepository.GetUserPortfolios(user);
            IEnumerable<Stock> filteredStock = userPortfolios.Where(p => p.Symbol.ToLower().Equals(symbol)).ToList();

            if (filteredStock.Count().Equals(1))
            {
                await _portfolioRepository.DeletePortfolioAsync(user, symbol);
            }
            else
            {
                return BadRequest("Stock not found for User");
            }
            return Ok("Deleted Successfully");




        }

    }
}