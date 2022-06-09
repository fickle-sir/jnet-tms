using JNet.Tms.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Linq;

namespace JNet.Tms
{
    internal static class XBodyModelBinderMvcOptionsExtentions
    {
        public static void ReplaceBodyModelBinder(this MvcOptions options)
        {
            var provider = options.ModelBinderProviders.FirstOrDefault(p => p is BodyModelBinderProvider);
            if (provider == null)
                throw new InvalidOperationException($"{typeof(BodyModelBinderProvider)} not found in ${nameof(MvcOptions)}.{nameof(MvcOptions.ModelBinderProviders)}");

            var index = options.ModelBinderProviders.IndexOf(provider);
            options.ModelBinderProviders.RemoveAt(index);
            options.ModelBinderProviders.Insert(index, new XBodyModelBinderProvider(provider as BodyModelBinderProvider));
        }
    }
}
