using System;
using e_me.Mobile.Services.DataStores;
using e_me.Shared.DTOs.User;

namespace e_me.Mobile.ViewModels
{
    public class UserDetailViewModel
    {
        private readonly UserDetailDataStore _userDetailDataStore;

        public UserDetailViewModel(UserDetailDataStore userDetailDataStore)
        {
            _userDetailDataStore = userDetailDataStore;
        }

        public UserDetailDto GetUserDetailDto()
        {
            return _userDetailDataStore.GetItem(new Guid());
        }

        public bool UpdateUserDetailDto(UserDetailDto userDetailDto)
        {
            return _userDetailDataStore.UpdateItemAsync(userDetailDto).Result;
        }
    }
}
