using System.ComponentModel.DataAnnotations;

namespace e_me.Model.Models
{
    public class Item : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
