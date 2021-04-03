using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_me.Model.Models
{
    public class DocumentTemplate : BaseModel
    {
        [Required]
        public Guid DocumentTypeId { get; set; }

        public byte[] File { get; set; }

        [ForeignKey("DocumentTypeId")]
        public DocumentType DocumentType { get; set; }
    }
}
