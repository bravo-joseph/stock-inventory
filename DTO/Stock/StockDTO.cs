using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resful_project.DTO.Comment;

namespace resful_project.DTO.Stock
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompnayName { get; set; } = string.Empty;

        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}