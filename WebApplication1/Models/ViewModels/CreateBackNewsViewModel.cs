using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class CreateBackNewsViewModel
    {
        [Display(Name = "標題")]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "是否顯示標籤")]
        public bool? IsTop { get; set; }

        [Display(Name = "發布資料之時間")]
        public DateTime? IssueTime { get; set; }
    }
    public class EditBackNewsViewModel
    {

        [Display(Name = "標題")]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "相片路徑")]
        public string PhotoPath { get; set; }

        [AllowHtml]
        [Display(Name = "CKeditor內容")]
        public string NewsCkContent { get; set; }

        [Display(Name = "是否顯示標籤")]
        public bool? IsShow { get; set; }

        [Display(Name = "是否顯示標籤")]
        public bool? IsTop { get; set; }

        [Display(Name = "發布資料之時間")]
        public DateTime? IssueTime { get; set; }
    }
}