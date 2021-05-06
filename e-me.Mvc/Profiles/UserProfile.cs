using System;
using AutoMapper;
using e_me.Model.Models;
using e_me.Shared.DTOs.User;

namespace e_me.Mvc.Profiles
{
    /// <summary>
    /// Auto-mapper profile for user-related models.
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// Default constructor for defining maps.
        /// </summary>
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserProfileDto>();
            CreateMap<UserProfileDto, User>();
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserRegistrationDto>();
            CreateMap<UserDetail, UserDetailDto>()
                .ForMember(p => p.BirthDate,
                    p => p.MapFrom(src => string.IsNullOrWhiteSpace(src.BirthYear)
                        ? default
                        : DateTime.Parse($"{src.BirthYear}-{src.BirthMonth}-{src.BirthDay}")));
            CreateMap<UserDetailDto, UserDetail>();
        }
    }
}
