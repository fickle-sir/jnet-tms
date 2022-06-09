using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JNet.Wbms
{
    [UIEditor(Size = UIEditorSize.lg, Span = 12)]
    [Display(Name = "运单货物", Order = 150)]
    public class WaybillCargo : IEntity, IEntId, IEntityTypeConfiguration<WaybillCargo>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public long ID { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "运单号")]
        public long WbID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        [MaxLength(50)]
        [Required(AllowEmptyStrings =false)]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "规格型号")]
        public string Specification { get; set; }

        [DefaultValue(0)]
        [Display(Name = "包装")]
        public virtual WaybillCargoPackage Package { get; set; }

        [Placeholder("长*宽*高")]
        [Required]
        [Dimension]
        [Display(Name = "尺寸(米)")]
        public Dimension<decimal> PackageDimension { get; set; }

        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        [Display(Name = "件数")]
        public int PackageNumber { get; set; }

        [Required]
        [Range(0.01, 9999999.99)]
        [Display(Name = "件重(KG)")]
        public decimal PackageWeight { get; set; }

        [Range(0.01, 9999999.99)]
        [Display(Name = "计费单价")]
        public decimal Price { get; set; }

        [Range(0.01, 9999999.99)]
        [Display(Name = "计费数量")]
        public decimal Number { get; set; }

        [DefaultValue(0)]
        [Display(Name = "计费单位")]
        public virtual WaybillCargoChargeUnit Unit { get; set; }

        [DefaultValue(0)]
        [Display(Name = "货损")]
        public decimal Damage { get; set; }

        void IEntityTypeConfiguration<WaybillCargo>.Configure(EntityTypeBuilder<WaybillCargo> builder)
        {
            builder.Property(p => p.Unit).HasConversion(new EnumToStringConverter<WaybillCargoChargeUnit>()).IsUnicode(false).HasMaxLength(20);
            builder.Property(p => p.Package).HasConversion(new EnumToStringConverter<WaybillCargoPackage>()).IsUnicode(false).HasMaxLength(20);
            builder.Property(p => p.PackageDimension).IsDimension().IsUnicode(false).HasMaxLength(64);
            // Not supported in EF?
            builder.Property(p => p.PackageWeight).HasPrecision(9, 2);
            builder.Property(p => p.Price).HasPrecision(9, 2);
            builder.Property(p => p.Number).HasPrecision(9, 2);
            builder.Property(p => p.Damage).HasPrecision(18, 2);
        }
    }
}
