using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    public class OneTimeAccessToken : BaseModel
    {
        public Guid UserDocumentId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Token { get; set; }

        public DateTime ValidTo { get; set; }

        public bool IsValid { get; set; }

        [ForeignKey("UserDocumentId")]
        public UserDocument UserDocument { get; set; }
    }
}
