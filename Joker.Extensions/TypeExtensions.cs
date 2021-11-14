using System.Collections;

namespace Joker.Extensions;

public static class TypeExtensions
{
    public static IDictionary ToDictionary(this Type type)
    {
        return type?.GetProperties().ToDictionary();
    }
}