using System.ComponentModel.DataAnnotations;

namespace JNet.Wbms
{
    public enum WaybillCargoPackage
    {
        [Display(Name = "无")]
        None,

        [Display(Name = "箱")]
        Box,

        [Display(Name = "其它")]
        Other = 999
    }
}
