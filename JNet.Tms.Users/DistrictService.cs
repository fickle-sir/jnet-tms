using System.Collections.Generic;
using System.Linq;

namespace JNet.Tms.Users
{
    public class DistrictService : EntityService<District, int>
    {
        public IList<District> GetChildren(int pid)
        {
            return EntitySet.Where(p => p.PID == pid).ToList();
        }
    }
}
