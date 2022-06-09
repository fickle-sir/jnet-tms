using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    public enum VehicleSteeringMode
    {
        [Display(Name = "方向盘")]
        Wheel,

        [Display(Name = "操纵杆")]
        Joystick,

        [Display(Name = "其它")]
        Unknown,
    }
}
