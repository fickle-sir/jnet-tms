using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace JNet.Tms.ModelBinding.Binders
{
    internal class EntIdModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var user = bindingContext.HttpContext.RequestServices.GetRequiredService<AuthorizedUserProvider>().GetUser();
            bindingContext.Result = ModelBindingResult.Success(user.EntId);
            return Task.CompletedTask;
        }
    }
}
