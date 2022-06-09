using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    /// <summary>
    /// 根据《中华人民共和国公安部机动车登记工作规范》，车辆登记车身颜色按“白、灰、黄、粉、红、紫、绿、蓝、棕、黑”归类录入。
    /// </summary>
    public enum VehicleColor
    {
        [Display(Name = "白色")]
        White,

        [Display(Name = "灰色")]
        Gray,

        [Display(Name = "黄色")]
        Yellow,

        [Display(Name = "粉色")]
        Pink,

        [Display(Name = "红色")]
        Red,

        [Display(Name = "紫色")]
        Purple,

        [Display(Name = "绿色")]
        Green,

        [Display(Name = "蓝色")]
        Blue,

        [Display(Name = "棕色")]
        Brown,

        [Display(Name = "黑色")]
        Black
    }
}
