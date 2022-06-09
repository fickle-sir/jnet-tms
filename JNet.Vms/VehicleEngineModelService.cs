using System;
using System.Linq;
using System.Collections.Generic;

namespace JNet.Vms
{
    public class VehicleEngineModelService : EntityService<VehicleEngineModel, int>
    {
        public override bool Delete(int[] id)
        {
            throw new NotImplementedException();

            //if (this.ComponyIdRequired(out int coId))
            //{
            //    if (DbContext.Set<VehicleModel>().Count(p => id.Contains(p.EngineModelID.Value)) > 0)
            //    {
            //        throw new HandledException($"正在使用中的发动机型号无法删除");
            //    }
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}

            //if (this.ComponyIdRequired(out int coId))
            //    return Delete(() => EntitySet.Where(p => id.Contains(p.ID)).Select(p => p.ID).ToArray(), null);
            //else
            //    return base.Delete(id);
        }

        public IDictionary<int, string> SearchPair(string value)
        {
            return this.EntitySet
                        .Where(EntityOwnerProvider)
                        .Where(p => p.ModelNo.Contains(value), !string.IsNullOrWhiteSpace(value))
                        .OrderBy(p => p.ModelNo)
                        .Take(20)
                        .Select(p => new { p.ID, p.ModelNo })
                        .AsEnumerable()
                        .ToDictionary(p => p.ID, p => p.ModelNo);
            //.ToCamalCaseObjectDictionary(p => p.ID, p => p.ModelNo);
        }
    }
}