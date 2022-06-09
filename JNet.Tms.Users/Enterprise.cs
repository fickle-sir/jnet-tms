using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JNet.Tms.Users
{
    public class Enterprise : IEntity, IUID, IEntityTypeConfiguration<Enterprise>
    {
        public int ID { get; set; }

        public int UID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "企业名称")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "联系人")]
        public string Contact { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]{6,11}$")]
        [Display(Name = "联系电话")]
        public string Tel { get; set; }

        [Required]
        [Display(Name = "地址信息")]
        public List<int> CascadeAddr { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "详细地址")]
        public string AssociateAddr { get; set; }

        public string Addr { get; set; }

        [Display(Name = "注册时间")]
        public DateTime CreatedTime { get; set; }

        void IEntityTypeConfiguration<Enterprise>.Configure(EntityTypeBuilder<Enterprise> builder)
        {
            builder.Property(p => p.CascadeAddr).HasMaxLength(50).HasConversion(new ListToStringConverter<List<int>>(null));
        }
    }
}
