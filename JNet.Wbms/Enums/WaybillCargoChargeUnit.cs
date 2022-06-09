using System.ComponentModel.DataAnnotations;

namespace JNet.Wbms
{
    public enum WaybillCargoChargeUnit
    {
        [Display(Name = "吨")]
        Ton,

        [Display(Name = "千克")]
        Kg,

        [Display(Name = "立方米")]
        Stere,
    }
}
