using System;
using System.Threading.Tasks;

namespace PerpetualEngine.Storage
{
    public interface IStorage
    {
        /// <summary>
        /// Persists a value with given key.
        /// </summary>
        /// <param name = "key">Key</param>
        /// <param name="value">if value is null, the key will be deleted</param>
        void Put(string key, string value);


        /// <summary>
        /// Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        string Get(string key);

        /// <summary>
        /// Retrives a value with given key. If key can not be found, defaultValue is returned instead of null.
        /// </summary>
        string Get(string key, string defaultValue);

        /// <summary>
        /// Delete the specified key.
        /// </summary>
        void Delete(string key);

        /// <summary>
        /// Determines whether the storage has the specified key.
        /// </summary>
        /// <returns><c>true</c> if this instance has the specified key; otherwise, <c>false</c>.</returns>
        /// <param name="key">Key.</param>
        bool HasKey(string key);

        /// <summary>
        /// Persists a value with given key.
        /// </summary>
        /// <param name="value">if value is null, the key will be deleted</param>
        /// <param name="key">Key.</param>
        Task PutAsync(string key, string value);


        /// <summary>
        /// Retrieves value with given key.
        /// </summary>
        /// <returns>null, if key can not be found</returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// Determines whether the storage has the specified key async.
        /// </summary>
        /// <returns><c>true</c> if this instance has the specified key; otherwise, <c>false</c>.</returns>
        /// <param name="key">Key.</param>
        Task<bool> HasKeyAsync(string key);


        /// <summary>
        /// Delete the specified key async.
        /// </summary>
        Task DeleteAsync(string key);


        /// <summary>
        /// Persists a complex value with given key via binary serialization async.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">must be serializable</param>
        Task PutAsync<T>(string key, T value);


        /// <summary>
        /// Persists a complex value with given key via binary serialization.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">must be serializable</param>
        void Put<T>(string key, T value);


        /// <summary>
        /// Get the specified key or the dafault value of type T if the key does not exist.
        /// </summary>
        /// <remarks>Use HasKey(key) beforehand if T is a Value Type and you do not want the default value.</remarks>
        /// <param name="key">Key.</param>
        /// <returns>deserialized complex type</returns>
        Task<T> GetAsync<T>(string key);


        /// <summary>
        /// Get the specified key or the dafault value of type T if the key does not exist.
        /// </summary>
        /// <remarks>Use HasKey(key) beforehand if T is a Value Type and you do not want the default value.</remarks>
        /// <param name="key">Key.</param>
        /// <returns>deserialized complex type</returns>
        T Get<T>(string key);


        /// <summary>
        /// Retrives a value with given key. If key can not be found, defaultValue is returned.
        /// </summary>
        /// <returns>deserialized complex type</returns>
        T Get<T>(string key, T defaultValue);

    }
}
