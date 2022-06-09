using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    public enum VehicleOwnerIdType
    {
        [Display(Name = "身份证")]
        IdCard,

        [Display(Name = "组织机构代码证书")]
        Org,
    }
}
