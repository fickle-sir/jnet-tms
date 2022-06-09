using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JNet.Wbms
{
    public class WaybillService : EntityService<Waybill, long>
    {
        // Use a 18 numbers sequence
        private static readonly SnowFlake Sequencer = new SnowFlake(new DateTime(2019, 8, 18).AddMonths(-10), 0, 0);

        public override bool Add(Waybill model)
        {
            if (model.Deduction > model.Amount)
                throw new AppException("减免金额不能大于运单金额");

            model.ID = Sequencer.NextId();
            model.ActualAmount = (model.Amount - model.Damage - model.Deduction);

            return base.Add(model);
        }

        public override bool Update(Waybill model)
        {
            if (model.Deduction > model.Amount)
                throw new AppException("减免金额不能大于运单金额");

            model.ActualAmount = (model.Amount - model.Deduction);
            return base.Update(model);
        }

        // 货运单
        public object GetPrintWaybill(long[] ids) => GetPrintWaybill(ids, "TplPrintWaybill.html");

        public object GetPrintRecept(long[] ids) => GetPrintWaybill(ids, "TplPrintRecept.html");

        private object GetPrintWaybill(long[] ids, string tplFile)
        {
            var tpl = GetRes(tplFile);
            var style = GetRes("TplDefaultStyle.css");
            tpl = tpl.Replace("${Style}", $"<style>{style}</style>");

            var cargos = DbContext.Set<WaybillCargo>()
                                  .Where(EntityOwnerProvider)
                                  .Where(p => ids.Contains(p.WbID))
                                  .Select(c => new WaybillCargoX(c))
                                  .ToList();

            var list = ids.Select(wid => new
            {
                ID = wid,
                Cargos = cargos.Where(c => c.WbID == wid).ToList()
            });

            return new
            {
                Tpl = tpl,
                Waybills = list
            };
        }

        private static string GetRes(string name)
        {
#if DEBUG
            var dir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName;
            var path = System.IO.Path.Combine(dir, "JNet.Wbms", name);
            var reader = new System.IO.StreamReader(path);
            var res = reader.ReadToEnd();
            reader.Dispose();
            return res;
#else
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var ns = assembly.GetName().Name;
            name = $"{ns}.{name}";

            var stream = assembly.GetManifestResourceStream(name);
            var reader = new System.IO.StreamReader(stream);
            var res = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();

            return res;
#endif
        }

        public class WaybillCargoX : WaybillCargo
        {
            [JsonConverter(typeof(DisplayNameJsonConverter))]
            public override WaybillCargoPackage Package { get; set; }

            [JsonConverter(typeof(DisplayNameJsonConverter))]
            public override WaybillCargoChargeUnit Unit { get; set; }

            public WaybillCargoX(WaybillCargo cargo)
            {
                ID = cargo.ID;
                WbID = cargo.WbID;
                Name = cargo.Name;
                Package = cargo.Package;
                PackageNumber = cargo.PackageNumber;
                PackageWeight = cargo.PackageWeight;
                PackageDimension = cargo.PackageDimension;
                Unit = cargo.Unit;
                Number = cargo.Number;
            }

            public class DisplayNameJsonConverter : JsonConverterFactory
            {
                public override bool CanConvert(Type typeToConvert) => true;

                public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
                {
                    var type = typeof(JsonEnumDisplayNameConverter<>).MakeGenericType(typeToConvert);
                    return Activator.CreateInstance(type) as JsonConverter;
                }

                public class JsonEnumDisplayNameConverter<T> : JsonConverter<T> where T : struct, Enum
                {
                    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                    {
                        throw new NotImplementedException();
                    }

                    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
                    {
                        writer.WriteStringValue(value.GetDisplayName());
                    }
                }
            }
        }
    }
}