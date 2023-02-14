using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDCG.WebApi.Models {
    public class EquitySpotMarketData : IEntity {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime BusinesssDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Currency)]
        public string Currency { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 3)]
        public string Ticker { get; set; }

        [StringLength(50)]
        public string LongName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Bid { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Ask { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Mid { get; set; }
    }
}
