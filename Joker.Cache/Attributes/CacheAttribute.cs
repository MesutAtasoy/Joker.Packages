using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Joker.Cache.Attributes
{
   public class CacheAttribute : Attribute, ICacheAttribute
    {
        public int TTL { get; set; }
        public string Key { get; set; }

        public async Task<string> OnBeforeAsync(MethodInfo targetMethod, object[] args, IJokerDistributedCache distributedCache)
        {
            Key = SetKey(Key, targetMethod, args);
            return await distributedCache.GetStringAsync(Key);
        }

        public string OnBefore(MethodInfo targetMethod, object[] args, IJokerDistributedCache distributedCache)
        {
            Key = SetKey(Key, targetMethod, args);
            return  distributedCache.Get(Key);
        }

        public async Task OnAfterAsync(MethodInfo targetMethod, object[] args, object value,
            IJokerDistributedCache distributedCache)
        {
            await distributedCache.SetAsync(Key, TTL, value);
        }

        public void OnAfter(MethodInfo targetMethod, object[] args, object value, IJokerDistributedCache distributedCache)
        {
            distributedCache.SetAsync(Key, TTL, value);
        }

        private string SetKey(string key,MethodInfo targetMethod, object[] args)
        {
            
            var parameters = targetMethod.GetParameters();

            if (parameters.Any())
            {
                var funcParams = parameters
                    .OrderBy(x=>x.Position)
                    .ToDictionary(parameter => parameter.Name, parameter => args[parameter.Position].ToString());
                
                if (string.IsNullOrEmpty(key))
                {
                    var cacheValues = string.Join(".", funcParams.Select(x => x.Value).ToList());
                    key = targetMethod.Name + cacheValues;
                }
                else
                {
                    foreach (var funcParam in funcParams)
                    {
                        key = key.Replace("{" + funcParam.Key + "}", funcParam.Value);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = targetMethod.Name;
                }
            }

            return key;
        }
    }
}