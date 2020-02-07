using System.ComponentModel.DataAnnotations;

namespace e_me.Shared.Models
{
    public class Item
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
