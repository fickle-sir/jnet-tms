using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace JNet
{
    public static class TypeExtentions
    {
        public static string GetDisplayName(this Type type)
        {
            return type.GetCustomAttribute<DisplayAttribute>(false)?.Name ??
                   type.GetCustomAttribute<DisplayNameAttribute>(false)?.DisplayName ??
                   type.Name;
        }
    }
}
