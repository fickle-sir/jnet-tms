using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JNet
{
    public static class EnumExtensions
    {
        public static string GetDisplayName<T>(this T value) where T : struct, Enum
        {
            var type = typeof(T);

            var eName = Enum.GetName(value);
            if (eName != null)
            {
                var attr = type.GetField(eName).GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
                return attr?.Name ?? eName;
            }
            return value.ToString();
        }
    }
}
