using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace JNet.Vms
{
    public abstract class VehicleDictService : EntityService<VehicleDict, int>
    {
        public abstract VehicleDictType Type { get; }

        public override VehicleDict GetOne(int id)
        {
            return base.GetOne(id);
        }

        public override IList<VehicleDict> GetAll() =>
            EntitySet.Where(EntityOwnerProvider).Where(p => p.Type == Type).ToList();

        public override PageList<VehicleDict> GetList(PageParams paras)
            => EntitySet.Where(EntityOwnerProvider).Where(p => p.Type == this.Type).OrderBy(p => p.Order).GetList(paras, PgOptions);

        public override bool Add(VehicleDict model) { model.Type = Type; return base.Add(model); }

        public override bool Update(VehicleDict model) { model.Type = Type; return base.Update(model); }

        public List<string> GetValues()
        {
            return EntitySet
                    .Where(EntityOwnerProvider)
                    .Where(p => p.Type == this.Type)
                    .OrderBy(p => p.Order)
                    .Select(p => p.Value)
                    .ToList();
        }
    }

    public class VehicleBrandService : VehicleDictService
    {
        public override VehicleDictType Type => VehicleDictType.Brand;
    }

    public class VehicleMfrService : VehicleDictService
    {
        public override VehicleDictType Type => VehicleDictType.Mfr;
    }
}