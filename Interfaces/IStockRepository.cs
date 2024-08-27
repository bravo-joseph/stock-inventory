using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resful_project.DTO.Stock;
using resful_project.Helper;
using resful_project.models;

namespace resful_project.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject? query);
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock> CreateStockAsync(Stock stock);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDTO stock);
        Task<Stock?> DeleteStockByIdAsync(int id);

        Task<bool> IsStockExistAsync(int id);
    }
}