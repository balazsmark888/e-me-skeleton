using AutoMapper;
using e_me.Model.Models;
using e_me.Shared.DTOs.Document;

namespace e_me.Mvc.Profiles
{
    /// <summary>
    /// Auto-mapper profile for document-related models.
    /// </summary>
    public class DocumentProfile : Profile
    {
        /// <summary>
        /// Default constructor for defining maps.
        /// </summary>
        public DocumentProfile()
        {
            CreateMap<DocumentType, DocumentTypeDto>();
            CreateMap<DocumentTypeDto, DocumentType>();

            CreateMap<UserDocument, UserDocumentDto>()
                .ForMember(d => d.DocumentTypeDisplayName,
                    opt => opt.MapFrom(s => s.DocumentTemplate.DocumentType.DisplayName))
                .ForMember(d => d.File,
                    opt => opt.MapFrom(s => default(string)));

            CreateMap<UserDocument, UserDocumentListItemDto>()
                .ForMember(d => d.DocumentTypeDisplayName,
                    opt => opt.MapFrom(s => s.DocumentTemplate.DocumentType.DisplayName));
        }
    }
}
