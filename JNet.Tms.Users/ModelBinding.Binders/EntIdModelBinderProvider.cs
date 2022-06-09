using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace JNet.Tms.ModelBinding.Binders
{
    internal class EntIdModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.MetadataKind == ModelMetadataKind.Property && context.Metadata.PropertyName == nameof(IEntId.EntId))
                return new EntIdModelBinder();

            return null;
        }
    }
}
