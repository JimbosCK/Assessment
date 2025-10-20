using System.ComponentModel.DataAnnotations;

namespace Assessment.Model {
    public class Country {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Capital { get; set; }

        public ICollection<Border> Borders { get; set; } = new List<Border>();
    }
}
