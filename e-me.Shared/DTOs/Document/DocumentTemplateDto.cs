using System;
using System.ComponentModel.DataAnnotations;

namespace e_me.Shared.DTOs.Document
{
    public class DocumentTemplateDto
    {
        [Required]
        public Guid DocumentTypeId { get; set; }

        public string File { get; set; }
    }
}
