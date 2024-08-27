using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using resful_project.data;
using resful_project.DTO.Response;
using resful_project.Interfaces;
using resful_project.models;

namespace resful_project.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> AddPortfolioAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            _context.SaveChanges();
            return portfolio;

        }

        public async Task<Portfolio> DeletePortfolioAsync(AppUser user, string symbol)
        {
            Portfolio portfoliomodel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.Stock.Symbol == symbol);
            if (portfoliomodel == null) return null;
            _context.Portfolios.Remove(portfoliomodel);
            await _context.SaveChangesAsync();
            return portfoliomodel;
        }

        public async Task<List<Stock>> GetUserPortfolios(AppUser user)
        {
            var ports = await _context.Portfolios.Where(p => p.AppUserId == user.Id).Select(stock => new Stock
            {
                Id = stock.Stock.Id,
                Symbol = stock.Stock.Symbol,
                CompnayName = stock.Stock.CompnayName,
                Purchase = stock.Stock.Purchase,
                MarketCap = stock.Stock.MarketCap,
                LastDiv = stock.Stock.LastDiv,
            }).ToListAsync();
            return ports;

        }
    }
}