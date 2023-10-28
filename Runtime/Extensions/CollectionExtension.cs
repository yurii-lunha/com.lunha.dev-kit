using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Random = UnityEngine.Random;

namespace Lunha.DevKit.Extensions
{
    [UsedImplicitly]
    public static class CollectionExtension
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection) =>
            collection.OrderBy(_ => Guid.NewGuid());

        public static IOrderedEnumerable<T> Shuffle<T>(this IOrderedEnumerable<T> collection) =>
            collection.OrderBy(_ => Guid.NewGuid());

        public static T RandomOne<T>(this IEnumerable<T> enumerable) where T : class
        {
            var array = enumerable as T[] ?? enumerable.ToArray();

            return !array.Any()
                ? null
                : array.ElementAt(Random.Range(0, array.Length));
        }

        public static T RandomOne<T>(this IEnumerable<T> enumerable, IEnumerable<T> except) where T : class
        {
            var array = enumerable.Except(except).ToArray();

            return !array.Any()
                ? null
                : RandomOne(array);
        }

        public static T RandomOne<T>(this IEnumerable<T> enumerable, T except) where T : class
        {
            return RandomOne(enumerable, new[] { except });
        }

        public static T RandomOne<T>(this ICollection<T> collection) where T : struct
        {
            if (!(collection?.Any() ?? false))
            {
                return default;
            }

            return collection.ElementAt(Random.Range(0, collection.Count));
        }

        public static T RandomOne<T>(this ICollection<T> collection, IEnumerable<T> except) where T : struct
        {
            if (!(collection?.Any() ?? false))
            {
                return default;
            }

            var array = collection.Except(except).ToArray();

            return !array.Any()
                ? default
                : RandomOne(array);
        }

        public static T RandomOne<T>(this ICollection<T> collection, T except) where T : struct
        {
            return RandomOne(collection, new[] { except });
        }

        /// <summary>
        /// Example:
        /// var randomMyEnum = typeof(MyEnum).RandomOne 'MyEnum'();
        /// </summary>
        public static T RandomOne<T>(this Type e) where T : Enum
        {
            var collection = e.ToEnumerable<T>().ToArray();
            return !collection.Any() ? default : collection.ElementAt(Random.Range(0, collection.Count()));
        }

        /// <summary>
        /// Example:
        /// var except = new List 'MyEnum' { MyEnum.Item1, MyEnum.Item2 };
        /// var randomMyEnum = typeof(MyEnum).RandomOne 'MyEnum'(except);
        /// </summary>
        public static T RandomOne<T>(this Type e, IEnumerable<T> except) where T : Enum
        {
            var collection = e.ToEnumerable<T>().ToList();
            if (!collection.Any()) return default;

            var array = collection.Except(except).ToArray();
            return !array.Any()
                ? default
                : array.ElementAt(Random.Range(0, array.Length));
        }
    }
}