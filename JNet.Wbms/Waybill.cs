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
    [UIEditor(Size = UIEditorSize.xl, Span = 8)]
    [UITable(MultiSelect = true)]
    [UIAction(Type = "Wb_PrintWaybill", Text = "打印运单", Order = 1501)]
    [UIAction(Type = "Wb_PrintRecept", Text = "打印回执单", Order = 1502)]
    [UITableAction(Text = "货物", NeedData = true, Target = nameof(WaybillCargo), FixedMap = new string[] { nameof(ID), nameof(WaybillCargo.WbID) })]
    [Display(Name = "运单", Order = 100)]
    public class Waybill : IEntity, IEntId, IEntityTypeConfiguration<Waybill>
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "运单号")]
        public long ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        #region 承运
        [UIData(Action = nameof(Vms.VehicleService.SearchPlateNos), Controller = nameof(Vms.Vehicle))]
        [UIHintSelect(Searchable = true, SerachOnServer = true)]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "承运车号")]
        public string PlateNo { get; set; }

        [UIData(
            ValueField = nameof(Vms.VehicleOwner.Name),
            LabelField = nameof(Vms.VehicleOwner.Name),
            Action = nameof(Vms.VehicleOwnerService.GetDriversByPlateNo),
            Controller = nameof(Vms.VehicleOwner)
            )]
        [UIHintSelect(
            Applies = new string[] { nameof(Vms.VehicleOwner.Phone), nameof(DriverContact) },
            Linkages = new string[] { nameof(PlateNo) }
            )]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "承运司机", AutoGenerateField = false)]
        public string Driver { get; set; }

        [MaxLength(11)]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "司机联系方式", AutoGenerateField = false)]
        public string DriverContact { get; set; }
        #endregion

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

        [UIHintRadio]
        [DefaultValue(0)]
        [Display(Name = "付款方式")]
        public WaybillPaymentMode PaymentMode { get; set; }

        [Range(0, 9999999.99)]
        [Display(Name = "运单金额")]
        public decimal Amount { get; set; }

        [Range(0, 9999999.99)]
        [ScaffoldColumn(false)]
        [Display(Name = "实收金额")]
        public decimal ActualAmount { get; set; }

        [Range(0, 9999999.99)]
        [DefaultValue(0)]
        [Display(Name = "减免金额")]
        public decimal Deduction { get; set; }

        [Range(0, 9999999.99)]
        [ScaffoldColumn(false)]
        [Display(Name = "货损金额")]
        public decimal Damage { get; set; }

        [Display(Name = "发货时间")]
        public DateTime? DeliveryTime { get; set; }

        [Display(Name = "收货时间")]
        public DateTime? ReceiptTime { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        [Display(AutoGenerateField = false)]
        public List<WaybillCargo> Cargos { get; set; }

        void IEntityTypeConfiguration<Waybill>.Configure(EntityTypeBuilder<Waybill> builder)
        {
            builder.Property(p => p.PaymentMode).HasConversion(new EnumToStringConverter<WaybillPaymentMode>()).IsUnicode(false).HasMaxLength(20);
            builder.Property(p => p.Amount).HasPrecision(9, 2);
            builder.Property(p => p.ActualAmount).HasPrecision(9, 2);
            builder.Property(p => p.Deduction).HasPrecision(9, 2);
            builder.Property(p => p.Damage).HasPrecision(9, 2);
        }
    }
}
