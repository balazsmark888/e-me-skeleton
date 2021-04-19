using System;
using AutoMapper;
using e_me.Model.Models;
using e_me.Shared.Communication;

namespace e_me.Mvc.Profiles
{
    /// <summary>
    /// Auto-mapper profile for encryption-related models.
    /// </summary>
    public class EncryptionProfile : Profile
    {
        /// <summary>
        /// Default constructor for defining maps.
        /// </summary>
        public EncryptionProfile()
        {
            CreateMap<EcdhKeyStore, UserEcdhKeyInformation>()
                .ForMember(d => d.ClientPublicKey,
                    o => o.MapFrom(s => Convert.ToBase64String(s.PeerPublicKey)))
                .ForMember(d => d.ServerPublicKey,
                    o => o.MapFrom(s => Convert.ToBase64String(s.PublicKey)))
                .ForMember(d => d.SharedKey,
                    o => o.MapFrom(s => Convert.ToBase64String(s.SharedKey)))
                .ForMember(d => d.DerivedHmacKey,
                    o => o.MapFrom(s => Convert.ToBase64String(s.DerivedHmacKey)))
                .ForMember(d => d.HmacKey,
                    o => o.MapFrom(s => Convert.ToBase64String(s.HmacKey)))
                .ForMember(d => d.IV,
                    o => o.MapFrom(s => Convert.ToBase64String(s.IV)));
        }
    }
}
