using JNet.Tms.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc;

namespace JNet.Tms
{
    public static class EntIdModelBinderMvcOptionsExtentions
    {
        public static void AddEntIdModelBinder(this MvcOptions options)
        {
            options.ModelBinderProviders.Insert(3, new EntIdModelBinderProvider());
        }
    }
}
