using System;
using System.Linq;
using System.Collections.Generic;

namespace JNet.Vms
{
    public class VehicleOwnerService : EntityService<VehicleOwner, int>
    {
        public override bool Add(VehicleOwner model)
        {
            var name = EntitySet
                        .Where(EntityOwnerProvider)
                        .Where(p => p.IdNo == model.IdNo)
                        .Select(p => p.Name)
                        .FirstOrDefault();
            if (name != null)
                throw new AppException($"证件号码：{model.IdNo}({name})已存在");

            return base.Add(model);
        }

        public override bool Update(VehicleOwner model)
        {
            var name = EntitySet
                        .Where(EntityOwnerProvider)
                        .Where(p => p.IdNo == model.IdNo)
                        .Where(p => p.ID != model.ID)
                        .Select(p => p.Name)
                        .FirstOrDefault();

            if (name != null)
                throw new AppException($"证件号码：{model.IdNo}({name})已存在");

            return base.Update(model);
        }

        public IList<object> GetDriversByPlateNo(string plateNo)
        {
            var query = from v in DbContext.Set<Vehicle>().Where(EntityOwnerProvider).Where(p => p.PlateNo == plateNo)
                        join r in DbContext.Set<VehicleOwnerRelation>() on v.ID equals r.VehicleID
                        join d in DbContext.Set<VehicleOwner>() on r.OwnerID equals d.ID
                        where r.IsDriver
                        select new { d.Name, d.Phone };

            return query.Select(d => d as object).ToList();
        }

        public IDictionary<int, string> SearchPairs(string value)
        {
            return EntitySet
                    .Where(EntityOwnerProvider)
                    .Where(p => p.Name.Contains(value), !string.IsNullOrEmpty(value))
                    .Take(20)
                    .Select(p => new { p.ID, p.Name, p.IdNo })
                    .ToDictionary(p => p.ID, p => $"{p.Name}({p.IdNo})");
        }
    }
}