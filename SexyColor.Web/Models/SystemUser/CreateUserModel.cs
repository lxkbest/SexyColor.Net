using Microsoft.AspNetCore.Mvc;
using SexyColor.BusinessComponents;
using System.ComponentModel.DataAnnotations;

namespace SexyColor.Web
{
    public class CreateUserModel
    {
        public long UserId { get; set; }

        [Display(Name = "用户名")]
        [StringLength(64, ErrorMessage = "用户名最大长度为64个字符")]
        [Required(ErrorMessage = "用户名为必填选项")]
        [Remote("ValidateUserName", "System", ErrorMessage = "不合法的用户名")]
        public string UserName { get; set; }

        [Display(Name = "昵称")]
        [Required(ErrorMessage = "请输入昵称")]
        [Remote("ValidateNickName", "System", ErrorMessage = "不合法的昵称")]
        [DataType(DataType.Text)]
        public string NickName { get; set; }

        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "密码为必填选项")]
        [Remote("ValidatePassword", "System", ErrorMessage = "不合法的密码")]
        public string Password { get; set; }

        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "确认密码与密码不符")]
        public string PassAgain { get; set; }

        [Display(Name = "帐号邮箱")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "邮箱为必填选项")]
        [Remote("ValidateEmail", "System", ErrorMessage = "不合法的帐号邮箱")]
        [StringLength(64, ErrorMessage = "邮箱最大长度为64个字符")]
        public string AccountEmail { get; set; }


        public User AsUser()
        {
            User user = User.New();
            user.AccountEmail = this.AccountEmail;
            user.UserName = this.UserName;
            user.NickName = this.NickName;
            return user;
        }
    }
}
