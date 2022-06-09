using System.Linq;
using System.Collections.Generic;

namespace JNet.Wbms
{
    public class ContractorService : EntityService<Contractor, int>
    {
        public List<Contractor> SearchConsignors(string value)
        {
            var result = EntitySet.Where(EntityOwnerProvider).Where(p => p.IsConsignor);

            if (!string.IsNullOrWhiteSpace(value))
                result = result.Where(p => p.Name.Contains(value));

            return result.Take(20).ToList();
        }

        public List<Contractor> SearchConsignees(string value)
        {
            var result = EntitySet.Where(EntityOwnerProvider).Where(p => p.IsConsignee);

            if (!string.IsNullOrWhiteSpace(value))
                result = result.Where(p => p.Name.Contains(value));

            return result.Take(20).ToList();
        }
    }
}