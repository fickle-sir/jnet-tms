using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JNet.Vms
{
    [Display(Name = "车辆车主")]
    public class VehicleOwnerRelation : IEntity, IEntId
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "所属企业", AutoGenerateField = false)]
        public int EntId { get; set; }

        [Display(Name = "车辆")]
        public int VehicleID { get; set; }

        [UIData(Action = nameof(VehicleOwnerService.SearchPairs), Controller = nameof(VehicleOwner))]
        [UIHintSelect(Searchable = true, SerachOnServer = true)]
        [DisplayField(nameof(OwnerName))]
        [Display(Name = "车主/司机")]
        public int OwnerID { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        [Display(AutoGenerateField = false)]
        public string OwnerName { get; set; }

        [Display(Name = "车主身份")]
        public bool IsOwner { get; set; }

        [Display(Name = "司机身份")]
        public bool IsDriver { get; set; }

        //[Required]
        //[Display(Name = "关系")]
        //public VehicleOwnerRelationType Relation { get; set; }

        //void IEntityTypeConfiguration<VehicleOwnerRelation>.Configure(EntityTypeBuilder<VehicleOwnerRelation> builder)
        //{
        //    builder.Property(p => p.Relation).HasConversion(new EnumToStringConverter<VehicleOwnerRelationType>()).HasMaxLength(20);
        //}
    }
}
