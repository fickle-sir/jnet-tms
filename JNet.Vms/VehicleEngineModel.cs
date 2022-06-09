using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace JNet.Vms
{
    [Display(Name = "发动机型号", Order = 130)]
    public class VehicleEngineModel : IEntity, IEntId, IEntityTypeConfiguration<VehicleEngineModel>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        /// <summary>
        /// 12：发动机型号
        /// </summary>
        [Required]
        [RegularExpression("^[\x21-\x7f]{1,50}$", ErrorMessage = "{0}格式不正确")]
        [StringLength(50)]
        [Display(Name = "型号编码")]
        public string ModelNo { get; set; }

        /// <summary>
        /// 13：燃料种类
        /// </summary>
        [Required]
        [Display(Name = "燃料种类")]
        public VehicleFuelType FuelType { get; set; }

        [NotMapped]
        [JsonIgnore]
        [UIHintComplexInput(nameof(Displacement), nameof(Power), Separator = "/")]
        [Display(Name = "排量/功率")]
        public object DisplacementOfPower { get; set; }

        /// <summary>
        /// 14.1 排量
        /// </summary>
        [Required]
        [Unit("ml")]
        [ScaffoldColumn(false)]
        [Display(Name = "排量", AutoGenerateField = false)]
        public int Displacement { get; set; }

        /// <summary>
        /// 14.2 功率
        /// </summary>
        [Required]
        [Unit("kw")]
        [ScaffoldColumn(false)]
        [Display(Name = "功率", AutoGenerateField = false)]
        public int Power { get; set; }

        void IEntityTypeConfiguration<VehicleEngineModel>.Configure(EntityTypeBuilder<VehicleEngineModel> builder)
        {
            builder.Property(p => p.FuelType).HasConversion(new EnumToStringConverter<VehicleFuelType>());
        }
    }
}
