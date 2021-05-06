using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_me.Mobile.Services.User;
using e_me.Shared.DTOs.User;

namespace e_me.Mobile.Services.DataStores
{
    public class UserDetailDataStore : IDataStore<UserDetailDto>
    {
        private readonly IUserService _userService;

        public UserDetailDataStore(IUserService userService)
        {
            _userService = userService;
        }

        public Task<bool> AddItemAsync(UserDetailDto item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemAsync(UserDetailDto item)
        {
            return await _userService.UpdateUserDetailAsync(item);
        }

        public Task<bool> DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailDto> GetItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserDetailDto GetItem(Guid id)
        {
            return _userService.GetUserDetailAsync(id).Result;
        }

        public Task<IEnumerable<UserDetailDto>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDetailDto> GetItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDetailDto> GetLocalItems()
        {
            throw new NotImplementedException();
        }
    }
}
