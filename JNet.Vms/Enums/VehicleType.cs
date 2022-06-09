using System;
using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    public enum VehicleType
    {
        #region 半挂车以“B”开头，分为重型、中型、轻型
        // 重型半挂车
        [Display(Name = "重型普通半挂车")]
        B11,
        [Display(Name = "重型厢式半挂车")]
        B12,
        [Display(Name = "重型罐式半挂车")]
        B13,
        [Display(Name = "重型平板半挂车")]
        B14,
        [Display(Name = "重型集装箱半挂车")]
        B15,
        [Display(Name = "重型自卸半挂车")]
        B16,
        [Display(Name = "重型特殊结构半挂车")]
        B17,
        [Display(Name = "重型仓栅式半挂车")]
        B18,
        [Display(Name = "重型旅居半挂车")]
        B19,
        [Display(Name = "重型专项作业半挂车")]
        B1A,
        [Display(Name = "重型低平板半挂车")]
        B1B,
        // 中型半挂车
        [Display(Name = "中型普通半挂车")]
        B21,
        [Display(Name = "中型厢式半挂车")]
        B22,
        [Display(Name = "中型罐式半挂车")]
        B23,
        [Display(Name = "中型平板半挂车")]
        B24,
        [Display(Name = "中型集装箱半挂车")]
        B25,
        [Display(Name = "中型自卸半挂车")]
        B26,
        [Display(Name = "中型特殊结构半挂车")]
        B27,
        [Display(Name = "中型仓栅式半挂车")]
        B28,
        [Display(Name = "中型旅居半挂车")]
        B29,
        [Display(Name = "中型专项作业半挂车")]
        B2A,
        [Display(Name = "中型低平板半挂车")]
        B2B,
        // 轻型半挂车
        [Display(Name = "轻型普通半挂车")]
        B31,
        [Display(Name = "轻型厢式半挂车")]
        B32,
        [Display(Name = "轻型罐式半挂车")]
        B33,
        [Display(Name = "轻型平板半挂车")]
        B34,
        [Display(Name = "轻型自卸半挂车")]
        B35,
        [Display(Name = "轻型仓栅式半挂车")]
        B36,
        [Display(Name = "轻型旅居半挂车")]
        B37,
        [Display(Name = "轻型专项作业半挂车")]
        B38,
        [Display(Name = "轻型低平板半挂车")]
        B39,
        #endregion

        #region 全挂车代码以“G”开头，分为重型、重型、轻型
        //重型全挂车
        [Display(Name = "重型普通全挂车")]
        G11,
        [Display(Name = "重型厢式全挂车")]
        G12,
        [Display(Name = "重型罐式全挂车")]
        G13,
        [Display(Name = "重型平板全挂车")]
        G14,
        [Display(Name = "重型集装箱全挂车")]
        G15,
        [Display(Name = "重型自卸全挂车")]
        G16,
        [Display(Name = "重型仓栅式全挂车")]
        G17,
        [Display(Name = "重型旅居全挂车")]
        G18,
        [Display(Name = "重型专项作业全挂车")]
        G19,
        [Display(Name = "中型普通全挂车")]
        G21,
        [Display(Name = "中型厢式全挂车")]
        G22,
        [Display(Name = "中型罐式全挂车")]
        G23,
        [Display(Name = "中型平板全挂车")]
        G24,
        [Display(Name = "中型集装箱全挂车")]
        G25,
        [Display(Name = "中型自卸全挂车")]
        G26,
        [Display(Name = "中型仓栅式全挂车")]
        G27,
        [Display(Name = "中型旅居全挂车")]
        G28,
        [Display(Name = "中型专项作业全挂车")]
        G29,
        // 轻型全挂车
        [Display(Name = "轻型普通全挂车")]
        G31,
        [Display(Name = "轻型厢式全挂车")]
        G32,
        [Display(Name = "轻型罐式全挂车")]
        G33,
        [Display(Name = "轻型平板全挂车")]
        G34,
        [Display(Name = "轻型自卸全挂车")]
        G35,
        [Display(Name = "轻型仓栅式全挂车")]
        G36,
        [Display(Name = "轻型旅居全挂车")]
        G37,
        [Display(Name = "轻型专项作业全挂车")]
        G38,
        #endregion

        #region 货车代码以“H”开头，分为重型、中型、轻型、微型、低速
        // 重型货车
        [Display(Name = "重型普通货车")]
        H11,
        [Display(Name = "重型厢式货车")]
        H12,
        [Display(Name = "重型封闭货车")]
        H13,
        [Display(Name = "重型罐式货车")]
        H14,
        [Display(Name = "重型平板货车")]
        H15,
        [Display(Name = "重型集装箱车")]
        H16,
        [Display(Name = "重型自卸货车")]
        H17,
        [Display(Name = "重型特殊结构货车")]
        H18,
        [Display(Name = "重型仓栅式货车")]
        H19,
        // 中型货车
        [Display(Name = "中型普通货车")]
        H21,
        [Display(Name = "中型厢式货车")]
        H22,
        [Display(Name = "中型封闭货车")]
        H23,
        [Display(Name = "中型罐式货车")]
        H24,
        [Display(Name = "中型平板货车")]
        H25,
        [Display(Name = "中型集装箱车")]
        H26,
        [Display(Name = "中型自卸货车")]
        H27,
        [Display(Name = "中型特殊结构货车")]
        H28,
        [Display(Name = "中型仓栅式货车")]
        H29,
        // 轻型货车
        [Display(Name = "轻型普通货车")]
        H31,
        [Display(Name = "轻型厢式货车")]
        H32,
        [Display(Name = "轻型封闭货车")]
        H33,
        [Display(Name = "轻型罐式货车")]
        H34,
        [Display(Name = "轻型平板货车")]
        H35,
        [Display(Name = "轻型自卸货车")]
        H37,
        [Display(Name = "轻型特殊结构货车")]
        H38,
        [Display(Name = "轻型仓栅式货车")]
        H39,
        // 微型货车
        [Display(Name = "微型普通货车")]
        H41,
        [Display(Name = "微型厢式货车")]
        H42,
        [Display(Name = "微型封闭货车")]
        H43,
        [Display(Name = "微型罐式货车")]
        H44,
        [Display(Name = "微型自卸货车")]
        H45,
        [Display(Name = "微型特殊结构货车")]
        H46,
        [Display(Name = "微型仓栅式货车")]
        H47,
        // 低速货车
        [Display(Name = "低速普通货车")]
        H51,
        [Display(Name = "低速厢式货车")]
        H52,
        [Display(Name = "低速罐式货车")]
        H53,
        [Display(Name = "低速自卸货车")]
        H54,
        [Display(Name = "低速仓栅式货车")]
        H55,
        #endregion

        #region 客车代码以“K”开头，分为大型、中型、小型、微型
        [Display(Name = "大型普通客车")]
        K11,
        [Display(Name = "大型双层客车")]
        K12,
        [Display(Name = "大型卧铺客车")]
        K13,
        [Display(Name = "大型铰接客车")]
        K14,
        [Display(Name = "大型越野客车")]
        K15,
        [Display(Name = "中型普通客车")]
        K21,
        [Display(Name = "中型双层客车")]
        K22,
        [Display(Name = "中型卧铺客车")]
        K23,
        [Display(Name = "中型铰接客车")]
        K24,
        [Display(Name = "中型越野客车")]
        K25,
        [Display(Name = "小型普通客车")]
        K31,
        [Display(Name = "小型越野客车")]
        K32,
        [Display(Name = "微型普通客车")]
        K41,
        [Display(Name = "微型越野客车")]
        K42,
        [Display(Name = "小型轿车")]
        K33,
        [Display(Name = "微型轿车")]
        K43,
        #endregion

        #region 牵引车代码以“Q”开头，分为重型、中型、轻型
        [Display(Name = "重型半挂牵引车")]
        Q11,
        [Display(Name = "重型全挂牵引车")]
        Q12,
        [Display(Name = "中型半挂牵引车")]
        Q21,
        [Display(Name = "中型全挂牵引车")]
        Q22,
        [Display(Name = "轻型半挂牵引车")]
        Q31,
        [Display(Name = "轻型全挂挂牵引车")]
        Q32,
        #endregion

        #region 专项作业车代码以“Z”开头，分为大型、中型、小型、微型、重型、轻型
        [Display(Name = "大型专项作业车")]
        Z11,
        [Display(Name = "中型专项作业车")]
        Z21,
        [Display(Name = "小型专项作业车")]
        Z31,
        [Display(Name = "微型专项作业车")]
        Z41,
        [Display(Name = "重型专项作业车")]
        Z51,
        [Display(Name = "轻型专项作业车")]
        Z71,
        #endregion

        /* -------------------------------
        //电车代码以“D”开头
        [Display(Name = "无轨电车")]
        D11,
        [Display(Name = "有轨电车")]
        D12,

        // 摩托车代码以“M”开头
        [Display(Name = "普通正三轮摩托车")]
        M11,
        [Display(Name = "轻便正三轮摩托车")]
        M12,
        [Display(Name = "正三轮载客摩托车")]
        M13,
        [Display(Name = "正三轮载货摩托车")]
        M14,
        [Display(Name = "侧三轮摩托车")]
        M15,
        [Display(Name = "普通二轮摩托车")]
        M21,
        [Display(Name = "轻便二轮摩托车")]
        M22,

        // 农用车代码以“N”开头
        [Display(Name = "三轮农用运输")]
        N11,
        [Display(Name = "四轮农用普通货车")]
        N21,
        [Display(Name = "四轮农用厢式货车")]
        N22,
        [Display(Name = "四轮农用罐式货车")]
        N23,
        [Display(Name = "四轮农用自卸货车")]
        N24,

        // 拖拉机代码以“T”开头
        [Display(Name = "大型轮式拖拉机")]
        T11,
        [Display(Name = "小型轮式拖拉机")]
        T21,
        [Display(Name = "手扶拖拉机")]
        T22,

        //轮式机械车代码以“J”开头
        [Display(Name = "手扶变形运输机")]
        T23,
        [Display(Name = "轮式装载机械")]
        J11,
        [Display(Name = "轮式挖掘机械")]
        J12,
        [Display(Name = "轮式平地机械")]
        J13
        */
    }
}
