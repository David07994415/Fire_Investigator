using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class CreateMemberShipFrontViewModel
    {
    }

    public class FrontMemberLogin
    {
        [Required(ErrorMessage ="必填欄位")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "必填欄位")]
        public string Password { get; set; }
    }

    public class FrontMemberRegister
    {
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "密碼驗證")]
        public string PasswordAgain { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "暱稱")]
        public string NickName { get; set; }
    }

}