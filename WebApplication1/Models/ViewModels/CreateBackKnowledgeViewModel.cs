using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class CreateBackKnowledgeViewModel
    {
        [Display(Name = "標題")]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "是否置頂標籤")]
        public bool? IsTop { get; set; }

        [Required]
        [Display(Name = "是否顯示標籤")]
        public bool? IsShow { get; set; }

        [Display(Name = "發布資料之時間")]
        public DateTime IssueTime { get; set; }
    }
}