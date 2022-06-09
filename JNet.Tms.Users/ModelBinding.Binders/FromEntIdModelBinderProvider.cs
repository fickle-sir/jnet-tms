using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JNet.Tms.ModelBinding.Binders
{
    /// <summary>
    /// An <see cref="IModelBinderProvider"/> for binding from the <see cref="AuthorizedUser.EntId"/>
    /// when a model has the BindingSource <see cref="BindingSource"/>.
    /// </summary>
    internal class FromEntIdModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// A <see cref="BindingSource"/> for <see cref="AuthorizedUser.EntId"/>.
        /// </summary>
        public static readonly BindingSource BindingSource = new BindingSource(
            "EntId",
            "EntId",
            isGreedy: false,
            isFromRequest: false);

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.BindingInfo.BindingSource == BindingSource)
                return new EntIdModelBinder();

            return null;
        }
    }
}
