using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Lunha.DevKit.Extensions
{
    [UsedImplicitly]
    public static class EnumExtension
    {
        public static IEnumerable<T> ToEnumerable<T>(this Type e) =>
            Enum.GetNames(e).Select(item => (T)Enum.Parse(e, item));
    }
}