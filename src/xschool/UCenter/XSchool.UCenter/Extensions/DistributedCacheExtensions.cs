using System;

namespace Microsoft.Extensions.Caching.Distributed
{
    public static class DistributedCacheExtensions
    {
        public static void SetString(this IDistributedCache cache, string key, string value, TimeSpan expire)
        {
            var options = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expire };
            cache.SetString(key, value, options);
        }
    }
}
