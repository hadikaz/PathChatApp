using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PathChatApp.Extensions
{
    /// <summary>
    /// Extension class to save data in cache or get cached data
    /// </summary>
    public static class DistributedCasheExtensions
    {
        /// <summary>
        /// save data in cache
        /// </summary>
        /// <typeparam name="T">data type</typeparam>
        /// <param name="cashe">cache server</param>
        /// <param name="recordId">cached data record key</param>
        /// <param name="data"> data </param>
        /// <param name="absoluteExpireTime">expiration time</param>
        /// <param name="unusedExpiredTime">expiration</param>
        /// <returns></returns>
        public static async Task SetRecordAsync<T>(this IDistributedCache cashe,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpiredTime = null)
        {
            // instance to configure cahce options
            var options = new DistributedCacheEntryOptions();

            // set the expiration date to be 60 seconds
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);

            options.SlidingExpiration = unusedExpiredTime;

            // convert the data to json object
            var jsonData = JsonSerializer.Serialize(data);

            // save the data in cache
            await cashe.SetStringAsync(recordId, jsonData, options);
        }
        /// <summary>
        /// Get cached data
        /// </summary>
        /// <typeparam name="T">data type</typeparam>
        /// <param name="cashe">cache server</param>
        /// <param name="recordId">cached data record key</param>
        /// <returns></returns>
        public static async Task<T> GetRecoredAsync<T>(this IDistributedCache cashe, string recordId)
        {
            // get the cached data
            var jsonData = await cashe.GetStringAsync(recordId);

            // if it is null, retrun an empty T
            if (jsonData is null)
            {
                return default(T);
            }
            // convert json data back to the desired data type, and return it.
            return JsonSerializer.Deserialize<T>(jsonData);

        }
    }
}
