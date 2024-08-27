using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resful_project.DTO.Stock;
using resful_project.models;

namespace resful_project.Mapper
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stockmodel)
        {

            return new StockDTO
            {

                Id = stockmodel.Id,
                Symbol = stockmodel.Symbol,
                CompnayName = stockmodel.CompnayName,
                Purchase = stockmodel.Purchase,
                LastDiv = stockmodel.LastDiv,
                Industry = stockmodel.Industry,
                MarketCap = stockmodel.MarketCap,
                Comments = stockmodel.Comments.Select(x => x.ToCommentDTO()).ToList()
            };
        }
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDTO createStockRequestDTO)
        {
            return new Stock
            {
                Symbol = createStockRequestDTO.Symbol,
                Purchase = createStockRequestDTO.Purchase,
                CompnayName = createStockRequestDTO.CompnayName,
                LastDiv = createStockRequestDTO.LastDiv,
                Industry = createStockRequestDTO.Industry,
                MarketCap = createStockRequestDTO.MarketCap,

            };

        }
        // public static Stock ToStockfromUpdateDTO(this UpdateStockRequestDTO updateStockRequestDTO){

        // }
    }
}