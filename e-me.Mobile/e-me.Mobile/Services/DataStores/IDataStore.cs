using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace e_me.Mobile.Services.DataStores
{
    public interface IDataStore<T> where T : class
    {
        Task<bool> AddItemAsync(T item);

        Task<bool> UpdateItemAsync(T item);

        Task<bool> DeleteItemAsync(Guid id);

        Task<T> GetItemAsync(Guid id);
        T GetItem(Guid id);

        Task<IEnumerable<T>> GetItemsAsync();

        IEnumerable<T> GetItems();

        IEnumerable<T> GetLocalItems();
    }
}
