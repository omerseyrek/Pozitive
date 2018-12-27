

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Core
{
    public class DefaultCacheProvider : ICacheProvider
    {
        private ObjectCache Cache { get { return MemoryCache.Default; } }

        public virtual object Get(string key)
        {
            return Cache[key];
        }

        public virtual void Set(string key, object data, int cacheTime)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };

            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsSet(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            if (Cache.Contains(key))
                Cache.Remove(key);
        }

        public void RemoveAuthCaches(string key)
        {
            var keyList = new List<string>();
            var var = (IEnumerable)Cache;
            foreach (DictionaryEntry cacheKey in var)
            {
                if (((string)cacheKey.Key).StartsWith(key))
                {
                    keyList.Add((string)cacheKey.Key);
                }
            }

            foreach (var s in keyList)
            {
                Cache.Remove(s);
            }
        }
    }
}
