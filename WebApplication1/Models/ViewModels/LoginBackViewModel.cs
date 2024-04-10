using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class LoginBackViewModel
    {
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "密碼")]
        public string Password { get; set; }
    }
}