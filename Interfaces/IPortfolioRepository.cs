using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resful_project.models;

namespace resful_project.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolios(AppUser user);
        Task<Portfolio> AddPortfolioAsync(Portfolio portfolio);
        Task<Portfolio> DeletePortfolioAsync(AppUser user, string symbol);
    }
}