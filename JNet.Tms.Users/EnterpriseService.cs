using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JNet.Tms.Users
{
    public class EnterpriseService : EntityServiceBase<Enterprise>
    {
        public object Reg(Enterprise ent)
        {
            // 当前不支持
            if (EntitySet.Any(p => p.UID == ent.UID))
                throw new AppException("已经注册过了");

            ent.CreatedTime = DateTime.Now;
            ent.Addr = GetCascadeAddr(ent.CascadeAddr) + ent.AssociateAddr;

            EntitySet.Add(ent);
            DbContext.SaveChanges();

            return new AuthorizedUser(ent.UID, ent.ID).GetJwtToken();
        }

        public Enterprise Get([FromEntId] int id)
        {
            return EntitySet.Where(p => p.ID == id).FirstOrDefault();
        }

        public bool Update(Enterprise ent)
        {
            ent.Addr = GetCascadeAddr(ent.CascadeAddr) + ent.AssociateAddr;
            return EntitySet.Where(p => p.ID == ent.ID && p.UID == ent.UID)
                            .Exclude(p => new { p.ID, p.UID, p.CreatedTime })
                            .Update(ent) > 0;
        }

        private string GetCascadeAddr(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new AppException("地址信息数据错误");

            IQueryable<District> dQuery = null;
            ids.Select(aid => DbContext.Set<District>()
                                        .Where(p => p.ID == aid)
                                        .Take(1)
                                        .Select(p => new District() { ID = p.ID, Name = p.Name })
                )
                .ToList()
                .ForEach(q => dQuery = dQuery == null ? q : dQuery.Concat(q));
            var districts = dQuery.ToList();

            if (ids.Count != districts.Count)
                throw new AppException("数据错误");

            var addr = string.Join("", ids.Select(did => districts.First(d => d.ID == did).Name));
            return addr;
        }
    }
}
