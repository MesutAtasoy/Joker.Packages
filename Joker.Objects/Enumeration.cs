using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Joker.Objects
{
    public abstract class Enumeration<TKey> : IComparable
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly TKey _value;
        private readonly string _displayName;

        protected Enumeration()
        {
        }

        protected Enumeration(TKey value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public TKey Value
        {
            get { return _value; }
        }

        public string DisplayName
        {
            get { return _displayName; }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration<TKey>, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration<TKey>;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = _value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }


        public static T FromValue<T>(TKey value) where T : Enumeration<TKey>, new()
        {
            var matchingItem = parse<T, TKey>(value, "value", item => item.Value.Equals(value));
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration<TKey>, new()
        {
            var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        private static T parse<T, K>(K value, string description, Func<T, bool> predicate)
            where T : Enumeration<TKey>, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem != null)
                return matchingItem;

            var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
            throw new ApplicationException(message);
        }

        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration<TKey>) other).Value);
        }
    }


    public abstract class Enumeration : Enumeration<Guid>
    {
        protected Enumeration(Guid value, string name)
            : base(value, name)
        {
        }
    }
}