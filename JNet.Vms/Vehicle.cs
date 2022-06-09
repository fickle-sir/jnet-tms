using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JNet.Vms
{
    [UITableAction(
        Text = "车主",
        NeedData = true,
        Target = nameof(VehicleOwnerRelation),
        FixedMap = new string[] { nameof(ID), nameof(VehicleOwnerRelation.VehicleID) })]
    [UIEditor(Size = UIEditorSize.lg, Span = 12)]
    [Display(Name = "车辆信息", Order = 110)]
    public class Vehicle : IEntity, IEntId, IEntityTypeConfiguration<Vehicle>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "号牌号码")]
        public string PlateNo { get; set; }

        [DefaultValue(0)]
        [Display(Name = "关系")]
        public VehicleRelation Relation { get; set; }

        /// <summary>
        /// 09：车架号
        /// </summary>
        [String(17, MinimumLength = 17, Constraint = StringConstraint.LetterAndNumber)]
        [Display(Name = "车架号")]
        public string VIN { get; set; }

        /// <summary>
        /// 07：车辆型号
        /// </summary>

        //[UIHintOperationGrid(nameof(VehicleModel))]
        [UIHintSelect(Searchable = true, SerachOnServer = true)]
        [UIData(Action = nameof(VehicleModelService.SearchPair), Controller = nameof(VehicleModel))]
        [RegularExpression("^[\x21-\x7f]{1,20}$")]
        [MaxLength(20)]
        [Display(Name = "车辆型号")]
        [DisplayField(nameof(ModelName))]
        public string ModelNo { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        [Display(AutoGenerateField = false)]
        public string ModelName { get; set; }

        /// <summary>
        /// 11：发动机号
        /// </summary>
        [RegularExpression("^[\x21-\x7f]{1,20}$", ErrorMessage = "{0}格式不正确")]
        [String(20)]
        [Display(Name = "发动机号", AutoGenerateField = false)]
        public string EngineNo { get; set; }

        /// <summary>
        /// 08：车身颜色
        /// </summary>
        [Display(Name = "车身颜色", AutoGenerateField = false)]
        public VehicleColor? Color { get; set; }

        /// <summary>
        /// 30：使用性质
        /// </summary>
        [DefaultValue(0)]
        [Display(Name = "使用性质")]
        public VehicleUsage Usage { get; set; }

        /// <summary>
        /// 31：车辆获得方式
        /// </summary>
        [Display(Name = "获得方式", AutoGenerateField = false)]
        public VehicleObtainWay? ObtainWay { get; set; }

        /// <summary>
        /// 32：车辆出厂日期
        /// </summary>
        [Display(Name = "出厂日期", AutoGenerateField = false)]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime? ManufactureDate { get; set; }

        /// <summary>
        /// 34：发证日期
        /// </summary>
        [Display(Name = "发证日期", AutoGenerateField = false)]
        public DateTime? IssueDate { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "状态", AutoGenerateField = false)]
        public string Status { get; set; }

        void IEntityTypeConfiguration<Vehicle>.Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property(p => p.Relation).HasConversion(new EnumToStringConverter<VehicleRelation>()).HasMaxLength(20);
            builder.Property(p => p.Color).HasConversion(new EnumToStringConverter<VehicleColor>()).HasMaxLength(20);
            builder.Property(p => p.Usage).HasConversion(new EnumToStringConverter<VehicleUsage>()).HasMaxLength(20);
            builder.Property(p => p.ObtainWay).HasConversion(new EnumToStringConverter<VehicleObtainWay>()).HasMaxLength(20);
        }
    }
}
