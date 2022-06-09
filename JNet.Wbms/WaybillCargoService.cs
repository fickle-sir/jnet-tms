using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JNet.Wbms
{
    public class WaybillCargoService : EntityService<WaybillCargo, long>
    {
        public override bool Add(WaybillCargo model)
        {
            return UpdateWaybill(
                model.WbID,
                cargos => cargos.Add(model),
                () => base.Add(model));
        }

        public override bool Update(WaybillCargo model)
        {
            return UpdateWaybill(
                model.WbID,
                cargos =>
                {
                    var cargo = cargos.Where(p => p.ID == model.ID).FirstOrDefault();
                    if (cargo == null)
                        throw new AppException("货物可能已经被删除，请刷新后重试");
                    cargo.Price = model.Price;
                    cargo.Number = model.Number;
                    cargo.Damage = model.Damage;
                },
                () => base.Update(model));
        }

        public override bool Delete(long[] id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (id.Length > 1)
                throw new AppException("不支持批量删除");

            var cid = id[0];
            var wbId = EntitySet.Where(p => p.ID == cid).Where(EntityOwnerProvider).Select(p => p.WbID).FirstOrDefault();

            return UpdateWaybill(
                wbId,
                cargos =>
                {
                    var cargo = cargos.Where(p => p.ID == cid).FirstOrDefault();
                    if (cargo == null)
                        throw new AppException("货物可能已经被删除，请刷新后重试");
                    cargos.Remove(cargo);
                },
                () => base.Delete(id));
        }

        private bool UpdateWaybill(long wbId, Action<List<WaybillCargo>> action, Func<bool> change)
        {
            var cargos = EntitySet
               .Where(p => p.WbID == wbId)
               .Where(EntityOwnerProvider)
               .Select(p => new WaybillCargo
               {
                   ID = p.ID,
                   Price = p.Price,
                   Number = p.Number,
                   Damage = p.Damage
               })
               .ToList();

            action.Invoke(cargos);

            var total = cargos.Sum(p => p.Price * p.Number);
            var damage = cargos.Sum(p => p.Damage);

            return UpdateWaybill(wbId, total, damage, change);
        }

        private bool UpdateWaybill(long wbId, decimal total, decimal damage, Func<bool> change)
        {
            var deduction = DbContext.Set<Waybill>().Where(p => p.ID == wbId).Select(p => p.Deduction).FirstOrDefault();
            if (deduction > total)
                throw new AppException("减免金额不能大于运单金额");

            var result = DbContext.Database.Transaction(trans =>
            {
                if (change.Invoke())
                {
                    var wbAffected = DbContext.Set<Waybill>()
                        .Where(p => p.ID == wbId)
                        .Select(p => new
                        {
                            p.Amount,
                            p.Damage,
                            p.ActualAmount
                        })
                        .Update(total, damage, (total - damage - deduction));

                    return wbAffected == 1;
                }

                return false;
            });

            return result;
        }

        public bool Check(WaybillCargo _)
        {
            return true;
        }
    }
}