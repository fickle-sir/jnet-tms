using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using JNet.Vms;
using JNet.Wbms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.Angular.Nz;
using Microsoft.Extensions.DependencyInjection;

namespace JNet.Tms.Web.Controllers
{
    [Route("[action]")]
    [Authorize]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private static readonly ConcurrentDictionary<Type, object> Models = new ConcurrentDictionary<Type, object>();
        private static readonly JsonSerializerOptions CamalCaseJsonOptions = new()
        {
            IgnoreNullValues = true,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        private static object __menus;

        public object GetMenus()
        {
            if (__menus == null)
            {
                var menus = CreateMenus().ToSerializableMenus();
                var bytes = JsonSerializer.SerializeToUtf8Bytes(menus, CamalCaseJsonOptions);
                __menus = JsonDocument.Parse(bytes);
            }
            return __menus;
        }

        // Action
        public object GetModel(string name, [FromServices] NzMetadataProvider metadataProvider)
        {
            var type = GetModelTypes().FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (type == null)
                return null;

            var model = Models.GetOrAdd(type, k =>
            {
                var metadata = metadataProvider.GetMetadataForType(type, Url);
                var bytes = JsonSerializer.SerializeToUtf8Bytes(metadata, CamalCaseJsonOptions);
                return JsonDocument.Parse(bytes);
            });

            return model;
        }

        private IList<Type> GetModelTypes()
        {
            return HttpContext.RequestServices.GetRequiredService<AppModule>().EntityTypes;
        }

        private static MenuCollection CreateMenus()
        {
            var vmsMenu = new Menu("车辆管理");
            var vmsMenus = CreateMenus(1, typeof(Vehicle), typeof(VehicleOwner), typeof(VehicleModel), typeof(VehicleBrand), typeof(VehicleMfr));
            vmsMenu.Children.AddRange(vmsMenus);

            var wbmsMenu = new Menu("运单管理");
            var wbmsMenus = CreateMenus(1, typeof(Waybill), typeof(Contractor));
            wbmsMenu.Children.AddRange(wbmsMenus);

            return new MenuCollection()
            {
                vmsMenu,
                wbmsMenu
            };
        }

        private static IEnumerable<Menu> CreateMenus(int action, params Type[] types)
        {
            return types.Select(type => new Menu(type.GetDisplayName(), null, type, GenerateUrl, action));
        }

        private static string GenerateUrl(Menu menu)
        {
            // Lower:97-122, Upper:65-90
            static bool IsUpperCase(char word) => word >= 65 && word <= 90;

            static string SplitToLower(string value)
            {
                if (string.IsNullOrEmpty(value))
                    return value;

                var parts = new List<string>();
                var part = new StringBuilder(value[0].ToString().ToLower()); // 
                var maxIndex = value.Length - 1;

                for (var index = 1; index < value.Length; index++)
                {

                    var word = value[index];

                    if (IsUpperCase(word))
                    {
                        bool isContinuous = index == maxIndex || IsUpperCase(value[index + 1]);

                        // for continuous upper-case char.
                        if (isContinuous)
                        {
                            part.Append(word.ToString().ToLower());
                        }
                        else
                        {
                            parts.Add(part.ToString());
                            part = new StringBuilder();
                            part.Append(word.ToString().ToLower());
                        }
                    }
                    else
                    {
                        part.Append(word);
                    }

                    if (index == maxIndex)
                        parts.Add(part.ToString());
                }

                return string.Join('_', parts);
            }

            var values = new string[] { SplitToLower(menu.ModelType.Name) };
            return string.Join('/', values).TrimStart('/');
        }

        private class TypeOrderComparer : IComparer<Type>
        {
            public int Compare(Type x, Type y)
            {
                var xOrder = x.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault()?.Order ?? 0;
                var yOrder = y.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault()?.Order ?? 0;

                return xOrder.CompareTo(yOrder);
            }
        }
    }
}
