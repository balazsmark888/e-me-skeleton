using System;

namespace e_me.Shared.DTOs.Document
{
    public class UserDocumentDto
    {
        public Guid Id { get; set; }

        public string DocumentTypeDisplayName { get; set; }

        public string File { get; set; }

        public string Hash { get; set; }

        public string Iv { get; set; }
    }
}
