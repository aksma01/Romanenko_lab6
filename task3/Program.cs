using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class FunctionCache<TKey, TResult>
    {
        private readonly Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();

        public delegate TResult FunctionDelegate(TKey key);

        private class CacheItem
        {
            public TResult Result { get; set; }
            public DateTime ExpirationTime { get; set; }
        }

        public TResult GetResult(TKey key, FunctionDelegate function, TimeSpan cacheDuration)
        {
            if (cache.TryGetValue(key, out var cacheItem) && DateTime.Now < cacheItem.ExpirationTime)
            {
                return cacheItem.Result;
            }
            TResult result = function(key);

            cache[key] = new CacheItem
            {
                Result = result,
                ExpirationTime = DateTime.Now.Add(cacheDuration)
            };

            return result;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            FunctionCache<string, int> cache = new FunctionCache<string, int>();

            FunctionCache<string, int>.FunctionDelegate getStringLength = key => key.Length;

            string input = "Hello, World!";
            int result = cache.GetResult(input, getStringLength, TimeSpan.FromMinutes(1));

            Console.WriteLine($"Довжина '{input}': {result}");

            result = cache.GetResult(input, getStringLength, TimeSpan.FromMinutes(1));

            Console.WriteLine($"Довжина '{input}' (cached): {result}");
        }
    }
}
