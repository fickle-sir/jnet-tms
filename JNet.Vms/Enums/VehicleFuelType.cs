using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    /// <summary>
    /// 机动车燃料类型
    /// </summary>
    public enum VehicleFuelType
    {
        /// <summary>
        /// 汽油
        /// </summary>
        [Display(Name = "汽油")]
        A,

        /// <summary>
        /// 柴油
        /// </summary>
        [Display(Name = "柴油")]
        B,

        /// <summary>
        /// 电
        /// </summary>
        [Display(Name = "电")]
        C,

        /// <summary>
        /// 混合油
        /// </summary>
        [Display(Name = "混合油")]
        D,

        /// <summary>
        /// 天然气
        /// </summary>
        [Display(Name = "天然气")]
        E,

        /// <summary>
        /// 液化石油气
        /// </summary>
        [Display(Name = "液化石油气")]
        F,

        /// <summary>
        /// 甲醇
        /// </summary>
        [Display(Name = "甲醇")]
        L,

        /// <summary>
        /// 乙醇
        /// </summary>
        [Display(Name = "乙醇")]
        M,

        /// <summary>
        /// 太阳能
        /// </summary>
        [Display(Name = "太阳能")]
        N,

        /// <summary>
        /// 混合动力
        /// </summary>
        [Display(Name = "混合动力")]
        O,

        /// <summary>
        /// 无，仅限挂车等无动力的
        /// </summary>
        [Display(Name = "无")]
        Y,

        /// <summary>
        /// 其它
        /// </summary>
        [Display(Name = "其它")]
        Z,
    }
}
