using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace JNet.Tms.ModelBinding.Binders
{
    internal class XBodyModelBinder : IModelBinder
    {
        private readonly BodyModelBinder _binder;

        public XBodyModelBinder(BodyModelBinder binder)
        {
            _binder = binder;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            await _binder.BindModelAsync(bindingContext);
            if (bindingContext.Result.IsModelSet)
            {
                if (bindingContext.Result.Model is IUID u)
                {
                    var userProvider = bindingContext.HttpContext.RequestServices.GetRequiredService<AuthorizedUserProvider>();
                    var user = userProvider.GetUser();
                    u.UID = user.UID;
                }

                if (bindingContext.Result.Model is IEntId ent)
                {
                    var userProvider = bindingContext.HttpContext.RequestServices.GetRequiredService<AuthorizedUserProvider>();
                    var user = userProvider.GetUser();
                    ent.EntId = user.EntId;
                }
            }
        }
    }
}
