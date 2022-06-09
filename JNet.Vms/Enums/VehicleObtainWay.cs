using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    public enum VehicleObtainWay
    {
        [Display(Name = "购买")]
        Purchase,

        [Display(Name = "调拨")]
        Allot,

        [Display(Name = "赠予")]
        Bestowal,

        [Display(Name = "继承")]
        Inherit,

        [Display(Name = "拍卖")]
        Auction,

        [Display(Name = "中奖")]
        Winning,

        // AssetReorganization
        [Display(Name = "资产重组")]
        AssetsReorg,

        // Overall asset sale
        [Display(Name = "资产整体买卖")]
        AssetsSale,

        // arbitration award
        [Display(Name = "仲裁裁决")]
        Arbitration,

        [Display(Name = "法院判决")]
        Judicial,

        // Debt repayment
        [Display(Name = "协议抵偿债务")]
        Debt,

        [Display(Name = "其它")]
        Other
    }
}
