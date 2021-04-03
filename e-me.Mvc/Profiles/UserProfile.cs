using AutoMapper;
using e_me.Business.DTOs.User;
using e_me.Model.Models;

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
            CreateMap<UserDetail, UserDetailDto>();
            CreateMap<UserDetailDto, UserDetail>();
        }
    }
}
