using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    [Table("Bulletin")]
    public class Bulletin
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "主題名稱")]
        public string Theme { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Ckeditor內容")]
        public string Content { get; set; }

        [Display(Name = "更新資料之使用者")]
        public int UpdateUser { get; set; }

        [Display(Name = "更新資料之時間")]
        public DateTime? UpdateTime { get; set; }


        [Display(Name = "創建資料之使用者")]
        public int CreateUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "創建資料之時間")]
        public DateTime? CreateTime { get; set; }


        [Display(Name = "回應留言成員")]
        public virtual ICollection<Message> MessageTable { get; set; }
    
    }
}