using Joker.Shared.Models.Base;
using Newtonsoft.Json;

namespace Joker.Shared.Extensions
{
    public static class BaseResponseModelExtensions
    {
        public static T ConvertResultTo<T>(this BaseResponseModel model)
            => (T)JsonConvert.DeserializeObject(model.Payload.ToString(), typeof(T));
    }
}