using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JNet.Wbms
{
    [Display(Name = "运单", Order = 100)]
    public class WaybillBatch : IEntity, IEntId, IEntityTypeConfiguration<WaybillBatch>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "运单号")]
        public long ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        #region 发货人
        [UIData(
            ValueField = nameof(Contractor.Name),
            LabelField = nameof(Contractor.Name),
            Action = nameof(ContractorService.SearchConsignors),
            Controller = nameof(Contractor)
            )]
        [UIHintSelect(
            Inputable = true,
            Searchable = true,
            SerachOnServer = true,
            Applies = new string[] { nameof(Contractor.Addr), nameof(ConsignorAddr), nameof(Contractor.Contact), nameof(ConsignorContact) }
            )]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "发货人")]
        public string Consignor { get; set; }

        [MaxLength(100)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "发货地址", AutoGenerateField = false)]
        public string ConsignorAddr { get; set; }
        [MaxLength(11)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "发货人联系方式", AutoGenerateField = false)]
        public string ConsignorContact { get; set; }
        #endregion

        #region 收货人
        [UIData(
            ValueField = nameof(Contractor.Name),
            LabelField = nameof(Contractor.Name),
            Action = nameof(ContractorService.SearchConsignees),
            Controller = nameof(Contractor)
            )]
        [UIHintSelect(
            Inputable = true,
            Searchable = true,
            SerachOnServer = true,
            Applies = new string[] { nameof(Contractor.Addr), nameof(ConsigneeAddr), nameof(Contractor.Contact), nameof(ConsigneeContact) }
            )]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "收货人")]
        public string Consignee { get; set; }

        [MaxLength(100)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "收货地址", AutoGenerateField = false)]
        public string ConsigneeAddr { get; set; }

        [MaxLength(11)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "收货人联系方式", AutoGenerateField = false)]
        public string ConsigneeContact { get; set; }
        #endregion

        [DefaultValue(0)]
        [Display(Name = "付款方式")]
        public WaybillPaymentMode PaymentMode { get; set; }

        [Display(Name = "运单金额")]
        public decimal Amount { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        [Display(AutoGenerateField = false)]
        public List<WaybillCargo> Cargos { get; set; }

        void IEntityTypeConfiguration<WaybillBatch>.Configure(EntityTypeBuilder<WaybillBatch> builder)
        {
            builder.Property(p => p.PaymentMode).HasConversion(new EnumToStringConverter<WaybillPaymentMode>()).IsUnicode(false).HasMaxLength(20);
            builder.Property(p => p.Amount).HasPrecision(9, 2);
        }
    }
}
