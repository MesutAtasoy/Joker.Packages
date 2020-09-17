using System.Reflection;
using System.Threading.Tasks;

namespace Joker.Cache.Attributes
{
    public interface ICacheAttribute
    {
        Task<string> OnBeforeAsync(MethodInfo targetMethod, object[] args, IJokerDistributedCache distributedCache);
        string OnBefore(MethodInfo targetMethod, object[] args, IJokerDistributedCache distributedCache);
        Task  OnAfterAsync(MethodInfo targetMethod,  object[] args, object value,  IJokerDistributedCache distributedCache);
        void  OnAfter(MethodInfo targetMethod,  object[] args, object value,  IJokerDistributedCache distributedCache);
    }
}