using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Normal işlem api , business , dataaccess şeklind egidiyor. Burada o yüzden IMemoryCache constructor ile enjecte etmedik
        //Çünkü bu saydığımız 3 yapı da bağlılık zinciri var fakar aspectler bu bağlılık zincirinin içinde değil.Zaten bu yüzden
        //servicetool yazdık.Core da aynı şekilde


        //Adapter Pattern , kendi sistemize uyarladık
        IMemoryCache _memoryCache;


        public MemoryCacheManager()
        {
            //insteas ın karşılığını alıyoruz
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();//using Microsoft.Extensions.DependencyInjection;
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }
        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); //sadece böyle bir anahtar varmı yokmu onu istiyorum , cache deki değeri verme karşılığı out _ şekilde
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        //Çalışma anında bellekten siler.Koda çalışma anında müdahale etme yada oluşturma gibi yapıları reflection ile yaparız..
        public void RemoveByPattern(string pattern)
        {
            //memorycache türünde olan,bellekte cachelendiğinde ,cache dataları EntriesCollection yapısına atar.
            //Bellekteki EntriesCollection bul
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // definisitonu _memorycache olanı bul
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
