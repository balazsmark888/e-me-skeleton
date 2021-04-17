using AutoMapper;
using e_me.Mobile.ViewModels;
using e_me.Shared.DTOs;
using e_me.Shared.DTOs.User;

namespace e_me.Mobile.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterViewModel, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, RegisterViewModel>();
            CreateMap<AuthDto, LoginViewModel>();
            CreateMap<LoginViewModel, AuthDto>();
        }
    }
}
