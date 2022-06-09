using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    public enum VehicleUsage
    {
        [Display(Name = "货运")]
        Freight,

        [Display(Name = "非营运")]
        NonCommercial,
    }
}
