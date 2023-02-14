using MDCG.WebApi.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDCG.WebApi.Models {
    public class FxSpotMarketData : IEntity {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string BaseCurrency { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string CounterCurrency { get; set; }

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
