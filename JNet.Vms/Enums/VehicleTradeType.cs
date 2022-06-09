using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    public enum VehicleTradeType
    {
        [Display(Name = "国产")]
        Domestic,
        [Display(Name = "进口")]
        Import
    }
}
