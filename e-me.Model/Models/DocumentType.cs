using System.ComponentModel.DataAnnotations;

namespace e_me.Model.Models
{
    public class DocumentType : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string DisplayName { get; set; }
    }
}
