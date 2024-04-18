using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    [Table("Business")]
    public class Business
    {
        [Key]                                  
        [Display(Name = "編號")] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "業務內容")]
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


        [Display(Name = "業務類型")]
        public int CategoryId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("CategoryId")]
        [Display(Name = "訂單表單")]
        public virtual BusinessCategory CategoryTable { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

    }
}