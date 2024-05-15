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
    [Table("Message")]
    public class Message
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "回應標題")]
        public string Title { get; set; }

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


        [Display(Name = "回應主題")]
        public int BulletinId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("BulletinId")]
        [Display(Name = "留言板表單")]
        public virtual Bulletin BulletinTable { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應
    }
}