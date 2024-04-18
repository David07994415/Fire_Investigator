using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    [Table("News")]
    public class News
    {

            [Key]
            [Display(Name = "編號")]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

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

            [Display(Name = "更新資料之使用者")]
            public int UpdateUser { get; set; }

            [Display(Name = "更新資料之時間")]
            public DateTime? UpdateTime { get; set; }

            [Display(Name = "創建資料之使用者")]
            public int CreateUser { get; set; }

            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            [Display(Name = "創建資料之時間")]
            public DateTime? CreateTime { get; set; }

    }
}