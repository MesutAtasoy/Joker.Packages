using System.Collections;
using System.Reflection;

namespace Joker.Extensions;

public static class PropertyInfoExtensions
{
    public static IDictionary ToDictionary(this IEnumerable<PropertyInfo> properties)
    {
        return properties?
            .Where(property => !string.IsNullOrWhiteSpace(property.GetValue(default) as string))
            .ToDictionary(property => property.Name.CamelCase(), property => property.GetValue(default) as string);
    }
}