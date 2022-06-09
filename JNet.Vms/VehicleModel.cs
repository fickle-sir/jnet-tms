using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace JNet.Vms
{
    [UIEditor(Size = UIEditorSize.xl, Span = 8)]
    [Display(Name = "车辆型号", Order = 120)]
    public class VehicleModel : IEntity, IEntId, IEntityTypeConfiguration<VehicleModel>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        #region 基本信息
        /// <summary>
        /// 07：车辆型号
        /// </summary>

        [RegularExpression("^[\x21-\x7f]{1,20}$")]
        [MaxLength(20)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "车辆型号")]
        public string ModelNo { get; set; }

        /// <summary>
        /// 06：车辆品牌
        /// </summary>
        [UIHintSelect(Searchable = true)]
        [UIData(Action = nameof(VehicleDictService.GetValues), Controller = nameof(VehicleBrand))]
        [Required]
        [MaxLength(20)]
        [Display(Name = "车辆品牌")]
        public string Brand { get; set; }

        /// <summary>
        /// 05：车辆类型
        /// </summary>
        [Display(Name = "车辆类型")]
        [UIHintSelect(Searchable = true)]
        public VehicleType Type { get; set; }

        /// <summary>
        /// 型号名称
        /// </summary>
        [MaxLength(30)]
        [Display(Name = "型号名称")]
        public string ModelName { get; set; }

        /// <summary>
        /// 15：制造厂名称
        /// </summary>
        [UIHintSelect(Searchable = true)]
        [UIData(Action = nameof(VehicleDictService.GetValues), Controller = nameof(VehicleMfr))]
        [MaxLength(30)]
        [Display(Name = "制造厂名称")]
        public string Manufacturer { get; set; }

        /// <summary>
        /// 10：国产/进口
        /// </summary>
        [Display(Name = "国产/进口")]
        public VehicleTradeType? TradeType { get; set; }

        /// <summary>
        /// 16：转向形式
        /// </summary>
        [Display(Name = "转向方式", AutoGenerateField = false)]
        public VehicleSteeringMode? SteeringMode { get; set; }
        #endregion

        #region 发动机
        /// <summary>
        /// 12：发动机型号
        /// </summary>
        [UIHintSelect(Searchable = true, SerachOnServer = true)]
        //[UIHintOperationGrid(nameof(VehicleEngineModel))]
        //[SourceUrl(nameof(VehicleEngineModelService.SearchPair), Controller = nameof(VehicleEngineModel))]
        [Display(Name = "发动机型号", AutoGenerateField = false)]
        public int? EngineModelID { get; set; }
        #endregion

        #region 轮距&轮胎
        /// <summary>
        /// use to generate grid column only.
        /// </summary>
        [NotMapped]
        [ScaffoldColumn(false)]
        [UIHintComplexInput(nameof(FrontTrack), nameof(RearTrack), Suffix = "mm")]
        [Display(Name = "轮距", AutoGenerateField = false)]
        public object WheelTrack { get; set; }

        /// <summary>
        /// 17.1/2：前轮距
        /// </summary>
        [ScaffoldColumn(false)]
        [Display(Name = "前轮距", ShortName = "前", AutoGenerateField = false)]
        public int? FrontTrack { get; set; }

        /// <summary>
        /// 17.2/2：后轮距
        /// </summary>
        [ScaffoldColumn(false)]
        [Display(Name = "后轮距", ShortName = "后", AutoGenerateField = false)]
        public int? RearTrack { get; set; }

        /// <summary>
        /// 18：轮胎数
        /// </summary>
        [Display(Name = "轮胎数", AutoGenerateField = false)]
        public int? TyreCount { get; set; }

        /// <summary>
        /// 19：轮胎规格
        /// </summary>
        [RegularExpression("^[\x21-\x7f]{1,20}$", ErrorMessage = "{0}格式不正确")]
        [StringLength(20)]
        [Display(Name = "轮胎规格", AutoGenerateField = false)]
        public string TyreSize { get; set; }
        #endregion

        #region 钢板弹簧&轴
        /// <summary>
        /// 19：钢板弹簧片数
        /// </summary>
        [Display(Name = "钢板弹簧片数", AutoGenerateField = false)]
        public int? PlateSpringCount { get; set; }

        /// <summary>
        /// 20：轴距
        /// </summary>
        [Display(Name = "轴距", AutoGenerateField = false)]
        public int? AxleSpread { get; set; }

        /// <summary>
        /// 22：轴数
        /// </summary>
        [Display(Name = "轴数", AutoGenerateField = false)]
        public int? AxleCount { get; set; }
        #endregion

        #region 货箱尺寸
        /// <summary>
        /// 货箱外廓尺寸
        /// </summary>
        [Dimension]
        [Display(Name = "货箱外廓尺寸", AutoGenerateField = false)]
        public Dimension<int> OutlineDimension { get; set; }

        /// <summary>
        /// 货箱内部尺寸
        /// </summary>
        [Dimension]
        [Display(Name = "货箱内部尺寸", AutoGenerateField = false)]
        public Dimension<int> InteriorDimension { get; set; }
        #endregion

        #region 质量&载客
        /// <summary>
        /// 25：总质量
        /// </summary>
        [Display(Name = "总质量", AutoGenerateField = false)]
        public int? TotalMass { get; set; }

        /// <summary>
        /// 26：核定载质量
        /// </summary>
        [Display(Name = "核定载质量", AutoGenerateField = false)]
        public int? RatifiedLoadMass { get; set; }

        /// <summary>
        /// 27：核定载客
        /// </summary>
        [Display(Name = "核定载客", AutoGenerateField = false)]
        public int? RatifiedSeatingCapacity { get; set; }

        /// <summary>
        /// 28：准牵引总质量
        /// </summary>
        [Display(Name = "准牵引总质量", AutoGenerateField = false)]
        public int? RatifiedTractionMass { get; set; }

        /// <summary>
        /// 29：驾驶室载客
        /// </summary>
        [Display(Name = "驾驶室载客", AutoGenerateField = false)]
        public int? RatifiedSeatingCapacityOfCab { get; set; }
        #endregion

        void IEntityTypeConfiguration<VehicleModel>.Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            builder.Property(p => p.Type).HasConversion(new EnumToStringConverter<VehicleType>());
            builder.Property(p => p.OutlineDimension).IsDimension().IsUnicode(false).HasMaxLength(64);
            builder.Property(p => p.InteriorDimension).IsDimension().IsUnicode(false).HasMaxLength(64);
            //builder.HasIndex(p => p.ModelNo).IsUnique();
        }
    }
}
