using System.ComponentModel.DataAnnotations;

namespace MDCG.WebApi.Models
{
    public class User : IEntity {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Email { get; set; }

        public bool CanRead { get; set; }

        public bool CanWrite { get; set; }

        public bool IsActive { get; set; }
    }
}
