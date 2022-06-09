using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JNet.Tms.ModelBinding.Binders
{
    /// <summary>
    /// An <see cref="IModelBinderProvider"/> for binding from the <see cref="AuthorizedUser.UID"/>
    /// when a model has the BindingSource <see cref="BindingSource"/>.
    /// </summary>
    internal class FromUIDModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// A <see cref="BindingSource"/> for <see cref="AuthorizedUser.UID"/>.
        /// </summary>
        public static readonly BindingSource BindingSource = new BindingSource(
            "UID",
            "UID",
            isGreedy: false,
            isFromRequest: false);

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.BindingInfo.BindingSource == BindingSource)
                return new UIDModelBinder();

            return null;
        }
    }
}
