using AutoMapper;
using e_me.Core.Communication;
using e_me.Model.Models;

namespace e_me.Mvc.Profiles
{
    /// <summary>
    /// Auto-mapper profiles for encryption-related models.
    /// </summary>
    public class EncryptionProfile : Profile
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EncryptionProfile()
        {
            CreateMap<EcdhKeyStore, UserEcdhKeyInformation>()
                .ForMember(dest => dest.ClientPublicKey,
                    src => src.MapFrom(s => s.OtherPartyPublicKey.ToByteArray()))
                .ForMember(dest => dest.PublicKey,
                    src => src.MapFrom(s => s.PublicKey.ToByteArray()));
        }
    }
}
