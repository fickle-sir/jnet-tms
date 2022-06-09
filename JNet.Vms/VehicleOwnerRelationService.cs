using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace JNet.Vms
{
    public class VehicleOwnerRelationService : EntityService<VehicleOwnerRelation, int>
    {
        public override bool Add(VehicleOwnerRelation model)
        {
            var count = EntitySet
                            .Where(p => p.VehicleID == model.VehicleID && p.OwnerID == model.OwnerID)
                            .Where(EntityOwnerProvider)
                            .Count();

            if (count > 0)
                throw new AppException("当前车主/司机已存在");

            return base.Add(model);
        }

        public override bool Update(VehicleOwnerRelation model)
        {
            // ensure the vehicle owned
            if (EntitySet.Where(p => p.VehicleID == model.VehicleID).Where(EntityOwnerProvider).Count() == 0)
                return false;

            var id = EntitySet
                .Where(p => p.VehicleID == model.VehicleID && p.OwnerID == model.OwnerID)
                .Where(EntityOwnerProvider)
                .Select(p => p.ID).FirstOrDefault();

            if (id != model.ID)
                throw new AppException("当前车主/司机已存在");

            return base.Update(model);
        }

        public override PageList<VehicleOwnerRelation> GetList(PageParams paras)
        {
            var query = from r in EntitySet.Where(paras.Filters).Where(EntityOwnerProvider)
                        join o in DbContext.Set<VehicleOwner>() on r.OwnerID equals o.ID into ot
                        from vo in ot.DefaultIfEmpty()
                        select new VehicleOwnerRelation()
                        {
                            ID = r.ID,
                            VehicleID = r.VehicleID,
                            OwnerID = r.OwnerID,
                            OwnerName = vo.Name,
                            IsOwner = r.IsOwner,
                            IsDriver = r.IsDriver
                        };
            return query.GetList(paras, PgOptions);
        }

        [NonAction]
        public override IList<VehicleOwnerRelation> GetAll() => base.GetAll();
    }
}