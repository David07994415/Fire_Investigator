using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class CreateBackIndexCoverViewModel
    {
        [Display(Name = "封面名稱")]
        [Required]
        [MaxLength(100)]
        public string CoverName { get; set; }

        [Display(Name = "相片路徑")]
        public string PhotoPath { get; set; }

        [Display(Name = "是否顯示標籤")]
        public bool IsShow { get; set; }
    }
}