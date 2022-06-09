using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JNet.Vms
{
    public class VehicleDict : IEntity, IEntId, IOrderableEntity, IEntityTypeConfiguration<VehicleDict>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "字典类型", AutoGenerateField = false)]
        public VehicleDictType Type { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "名称")]
        public string Value { get; set; }

        [DefaultValue(1)]
        [Display(Name = "排序")]
        public int Order { get; set; }

        void IEntityTypeConfiguration<VehicleDict>.Configure(EntityTypeBuilder<VehicleDict> builder)
        {
            builder.Property(p => p.Type).HasConversion(new EnumToStringConverter<VehicleDictType>());
        }
    }

    [Display(Name = "车辆品牌", Order = 510)]
    public class VehicleBrand : VehicleDict { }

    [Display(Name = "车辆制造厂", Order = 520)]
    public class VehicleMfr : VehicleDict { }
}
