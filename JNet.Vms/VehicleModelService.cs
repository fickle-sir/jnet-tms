using System;
using System.Linq;

namespace JNet.Vms
{
    public class VehicleModelService : EntityService<VehicleModel, int>
    {
        public override bool Add(VehicleModel model)
        {
            if (this.EntitySet.Where(p => p.ModelNo == model.ModelNo).Where(EntityOwnerProvider).Count() > 0)
                throw new AppException($"型号{model.ModelNo}已存在");

            return base.Add(model);
        }

        public override bool Update(VehicleModel model)
        {
            var existsId = this.EntitySet.Where(p => p.ModelNo == model.ModelNo).Where(EntityOwnerProvider).Select(p => p.ID).FirstOrDefault();
            if (model.ID != existsId)
                throw new AppException($"型号{model.ModelNo}已存在");

            return base.Update(model);
        }

        public override bool Delete(int[] id)
        {
            throw new NotImplementedException();
            //var coId = this.GetComponyId();

            //var query = from v in DbContext.Set<Vehicle>()
            //            where
            //            // 只查询指定公司的车辆
            //            v.CoID == coId &&
            //            // 只查询指定公司的车辆型号
            //            (from vm in DbContext.Set<VehicleModel>().Where(p => p.CoID == coId && id.Contains(p.ID)) select vm.ModelNo).Contains(v.ModelNo)
            //            select v;

            //if (query.Count() > 0)
            //    throw new HandledException($"正在使用中的车辆型号无法删除");

            //return base.Delete(id);
        }

        public object SearchPair(string value)
        {
            var result = EntitySet
                            .Where(EntityOwnerProvider)
                            .Where(p => p.ModelNo.Contains(value), !string.IsNullOrWhiteSpace(value))
                            .Select(p => new
                            {
                                p.ModelNo,
                                p.ModelName,
                                p.Brand,
                                p.Type
                            })
                            .Take(20)
                            .ToDictionary(p => p.ModelNo, p => $"{p.Brand}-{p.ModelName ?? p.Type.GetDisplayName()}({p.ModelNo})");
            return result;
        }
    }
}