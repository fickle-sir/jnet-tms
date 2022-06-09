using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    public enum VehicleDictType
    {
        [Display(Name = "车辆品牌")]
        Brand,

        /// <summary>
        /// Manufacturer
        /// </summary>
        [Display(Name = "车辆制造厂")]
        Mfr,
    }
}
