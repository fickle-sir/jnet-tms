using System.ComponentModel.DataAnnotations;

namespace JNet.Wbms
{
    public enum WaybillPaymentMode
    {
        [Display(Name = "收货方付")]
        Consignee,

        [Display(Name = "发货方付")]
        Consignor,
    }
}
