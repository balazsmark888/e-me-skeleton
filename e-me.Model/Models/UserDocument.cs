using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    public class UserDocument : BaseModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid DocumentTemplateId { get; set; }

        [Required]
        [Encrypted]
        public byte[] File { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("DocumentTemplateId")]
        public DocumentTemplate DocumentTemplate { get; set; }
    }
}
