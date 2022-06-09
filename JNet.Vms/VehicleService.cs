using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JNet.Vms
{
    public class VehicleService : EntityService<Vehicle, int>
    {
        public override bool Add(Vehicle model)
        {
            if (EntitySet.Where(p => p.PlateNo == model.PlateNo).Where(EntityOwnerProvider).Any())
                throw new AppException($"号牌号码'{model.PlateNo}'已存在");

            if (model.VIN != null && EntitySet.Where(p => p.VIN == model.VIN).Where(EntityOwnerProvider).Any())
                throw new AppException($"车架号'{model.VIN}'已存在");

            return base.Add(model);
        }

        public override bool Update(Vehicle model)
        {
            var existsId = EntitySet.Where(p => p.PlateNo == model.PlateNo)
                                    .Where(EntityOwnerProvider)
                                    .Select(p => p.ID)
                                    .FirstOrDefault();
            if (existsId > 0 && model.ID != existsId)
                throw new AppException($"号牌号码'{model.PlateNo}'已存在");

            if (model.VIN != null)
            {
                existsId = EntitySet.Where(p => p.VIN == model.VIN)
                                    .Where(EntityOwnerProvider)
                                    .Select(p => p.ID)
                                    .FirstOrDefault();
                if (existsId > 0 && model.ID != existsId)
                    throw new AppException($"车架号'{model.VIN}'已存在");
            }

            return base.Update(model);
        }

        public override PageList<Vehicle> GetList(PageParams paras)
        {
            var query = from v in EntitySet.Where(EntityOwnerProvider)
                        join m in DbContext.Set<VehicleModel>()
                        on new { v.EntId, v.ModelNo } equals new { m.EntId, m.ModelNo } into mt
                        from vm in mt.DefaultIfEmpty()
                        select new Vehicle
                        {
                            ID = v.ID,
                            EntId = v.EntId,
                            PlateNo = v.PlateNo,
                            Relation = v.Relation,
                            VIN = v.VIN,
                            ModelNo = v.ModelNo,
                            ModelName = CombineModelName(vm.ModelName, vm.Brand, vm.Type, v.ModelNo),
                            Color = v.Color,
                            EngineNo = v.EngineNo,
                            Usage = v.Usage,
                            ObtainWay = v.ObtainWay,
                            ManufactureDate = v.ManufactureDate,
                            IssueDate = v.ManufactureDate
                        };

            return query.GetList(paras, PgOptions);
        }

        public string[] SearchPlateNos(string value)
        {
            return this.EntitySet
                        .Where(EntityOwnerProvider)
                        .Where(p => p.PlateNo.Contains(value), !string.IsNullOrEmpty(value))
                        .Select(p => p.PlateNo)
                        .ToArray();
        }

        private static string CombineModelName(string modelName, string brand, VehicleType? type, string modelNo)
        {
            if (modelNo != null)
            {
                if (brand != null && modelName != null)
                    return $"{brand}-{modelName}({modelNo})";
                else if (brand != null && type.HasValue)
                    return $"{brand}-{type.Value.GetDisplayName()}({modelNo})";
                else
                    return modelNo;
            }
            return null;
        }
    }
}