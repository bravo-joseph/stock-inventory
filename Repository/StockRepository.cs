using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using resful_project.data;
using resful_project.DTO.Stock;
using resful_project.Helper;
using resful_project.Interfaces;
using resful_project.models;

namespace resful_project.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStockByIdAsync(int id)
        {
            var stockmodel = await _context.Stock.FirstOrDefaultAsync(Stock => Stock.Id == id);
            if (stockmodel == null)
            {
                return null;
            }
            _context.Stock.Remove(stockmodel);
            await _context.SaveChangesAsync();
            return stockmodel;
        }

        public async Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
            var stocks = _context.Stock.Include(comm => comm.Comments).ThenInclude(user => user.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompnayName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDesc ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            var stockmodel = await _context.Stock.FirstOrDefaultAsync(s => s.Symbol == symbol);
            if (stockmodel == null) return null;
            return stockmodel;
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            var stockmodel = await _context.Stock.Include(comm => comm.Comments).ThenInclude(user => user.AppUser).FirstOrDefaultAsync(Stock => Stock.Id == id);
            return stockmodel;
        }

        public async Task<bool> IsStockExistAsync(int id)
        {
            bool StockExists = await _context.Stock.AnyAsync(stock => stock.Id == id);
            return StockExists;
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDTO stockDto)
        {
            var existingstock = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
            if (existingstock == null)
            {
                return null;
            }
            existingstock.Purchase = stockDto.Purchase;
            existingstock.MarketCap = stockDto.MarketCap;
            existingstock.LastDiv = stockDto.LastDiv;
            existingstock.CompnayName = stockDto.CompnayName;
            existingstock.Symbol = stockDto.Symbol;

            await _context.SaveChangesAsync();
            return existingstock;

        }
    }
}
