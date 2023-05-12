namespace Boek.Repository.Interfaces
{
    public interface ICacheProvider
    {
        TimeSpan Expire { set; get; }

        Task<T> GetValueAsync<T>(string key);

        T GetValue<T>(string key);

        Task<bool> SetValueAsync(string key, object value);

        bool SetValue(string key, object value);

        Task<bool> RemoveAsync(string key);

        bool Remove(string key);

        Task<bool> IsExistAsync(string key);

        bool IsExist(string key);
    }
}