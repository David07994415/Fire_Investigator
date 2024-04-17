using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    [Table("WebContent")]
    public class WebContent
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "HTML內容")]
        public string HTMLContent { get; set; }

        [Required]
        [Display(Name = "目錄外鍵")]
        public int DirectroyId { get; set; }
        [ForeignKey("DirectroyId")]
        [Display(Name = "目錄表單")]
        public virtual Directory DirectoryTable { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應


        [Display(Name = "更新資料之使用者")]
        public int UpdateUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "更新資料之時間")]
        public DateTime? UpdateTime { get; set; }

        [Display(Name = "創建資料之使用者")]
        public int CreateUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "創建資料之時間")]
        public DateTime? CreateTime { get; set; }

    }
}