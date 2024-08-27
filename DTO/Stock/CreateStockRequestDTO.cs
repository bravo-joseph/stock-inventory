using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using resful_project.DTO.Comment;

namespace resful_project.DTO.Stock
{
    public class CreateStockRequestDTO
    {
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10)]
        public string CompnayName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10)]
        public string Industry { get; set; } = string.Empty;

        [Range(1, 500000000)]
        public long MarketCap { get; set; }

    }
}