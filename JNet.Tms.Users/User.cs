using System;
using System.ComponentModel.DataAnnotations;

namespace JNet.Tms.Users
{
    public class User : IEntity
    {
        [Display(Name = "用户ID")]
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^[\u4e00-\u9fa5_a-zA-Z0-9]{2,16}$", ErrorMessage = "用户名应为2-16个字符")]
        [Display(Name = "用户名")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[\x21-\x7f]{6,16}$", ErrorMessage = "密码应为6-16个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [RegularExpression("^[1][3-9][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        [Display(Name = "手机号码")]
        public string MobilePhone { get; set; }

        [EmailAddress]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Display(Name = "用户类型")]
        public string UserType { get; set; }

        [Display(Name = "注册时间")]
        public DateTime RegTime { get; set; }

        [Display(Name = "注册IP")]
        public string RegIP { get; set; }
    }
}
