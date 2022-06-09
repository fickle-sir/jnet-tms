using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JNet.Vms
{
    [Display(Name = "车主/司机", Order = 110)]
    public class VehicleOwner : IEntity, IEntId
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "姓名/名称")]
        public string Name { get; set; }

        [Required]
        [DefaultValue(0)]
        [Display(Name = "证件类型")]
        public VehicleOwnerIdType IdType { get; set; }

        [Required]
        [StringLength(18)]
        [Display(Name = "证件号码")]
        public string IdNo { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "联系电话")]
        public string Phone { get; set; }

        [StringLength(100)]
        [Display(Name = "联系地址")]
        public string Address { get; set; }
    }
}
