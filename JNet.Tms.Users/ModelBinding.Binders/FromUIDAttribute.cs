using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using JNet.Tms.ModelBinding.Binders;

namespace JNet.Tms
{
    /// <summary>
    /// Specifies that an action parameter should be bound using the <see cref="FromUIDModelBinderProvider"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class FromUIDAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => FromUIDModelBinderProvider.BindingSource;
    }
}
