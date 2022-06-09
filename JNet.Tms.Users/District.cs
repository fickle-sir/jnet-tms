using System;
using System.ComponentModel.DataAnnotations;

namespace JNet.Tms.Users
{
    public class District : IEntity
    {
        [Display(Name = "编码")]
        public int ID { get; set; }

        public int PID { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public bool IsLeaf { get; set; }
    }
}
