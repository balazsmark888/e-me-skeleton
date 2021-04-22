﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_me.Mobile.Services.DataStores
{
    public interface IDataStore<T> where T : class
    {
        Task<bool> AddItemAsync(T item);

        Task<bool> UpdateItemAsync(T item);

        Task<bool> DeleteItemAsync(string id);

        Task<T> GetItemAsync(string id);

        Task<IEnumerable<T>> GetItemsAsync();

        IEnumerable<T> GetItems();

        IEnumerable<T> GetLocalItems();
    }
}
