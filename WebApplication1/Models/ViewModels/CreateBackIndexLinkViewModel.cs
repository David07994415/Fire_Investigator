using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class CreateBackIndexLinkViewModel
    {
        [Display(Name = "連結名稱")]
        [Required]
        [MaxLength(100)]
        public string LinkName { get; set; }

        [Display(Name = "連結網址")]
        [Required]
        public string LinkPath { get; set; }

        [Required]
        [Display(Name = "是否顯示標籤")]
        public bool? IsShow { get; set; }

    }
}