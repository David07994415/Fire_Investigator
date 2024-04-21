using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("Knowledge")]
    public class Knowledge:ModelBase
    {
        [Required]
        [Display(Name ="標題")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "檔案名稱")]
        public string FileName { get; set; }

        [Display(Name = "是否顯示標籤")]
        public bool? IsShow { get; set; }

        [Display(Name = "是否置頂標籤")]
        public bool? IsTop { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")] //進行編輯操作時能夠看到適當格式的日期時間
        //[DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        [Display(Name = "發布資料之時間")]
        public DateTime? IssueTime { get; set; }

    }
}