using System.ComponentModel.DataAnnotations;

namespace Assessment.Model {
    public class Border {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string BorderCode { get; set; }

        [Required]
        public Guid CountryId { get; set; }

        [Required]
        public Country Country { get; set; }
    }
}
