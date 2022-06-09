using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JNet.Wbms
{
    [Display(Name = "收/发货人", Order = 200)]
    public class Contractor : IEntity, IEntId
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "运单号", AutoGenerateField = false)]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "地址")]
        public string Addr { get; set; }

        [Required]
        [MaxLength(11)]
        [Display(Name = "联系方式")]
        public string Contact { get; set; }

        [UIHintSwitch]
        [Display(Name = "是否发货人")]
        public bool IsConsignor { get; set; }

        [UIHintSwitch]
        [Display(Name = "是否收货人")]
        public bool IsConsignee { get; set; }
    }
}
