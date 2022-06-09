using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    public enum VehicleRelation
    {
        [Display(Name = "挂靠")]
        Affiliate,

        [Display(Name = "租用")]
        Lease,

        [Display(Name ="自购")]
        Own
    }
}
