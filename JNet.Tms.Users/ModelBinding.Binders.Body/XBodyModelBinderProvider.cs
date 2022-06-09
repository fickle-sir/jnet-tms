using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace JNet.Tms.ModelBinding.Binders
{
    internal class XBodyModelBinderProvider : IModelBinderProvider
    {
        private readonly BodyModelBinderProvider _provider;

        public XBodyModelBinderProvider(BodyModelBinderProvider provider)
        {
            _provider = provider;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var binder = _provider.GetBinder(context);
            if (binder == null)
                return null;
            return new XBodyModelBinder(binder as BodyModelBinder);
        }
    }
}
