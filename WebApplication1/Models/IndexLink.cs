using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    [Table("IndexLink")]
    public class IndexLink
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "連結名稱")]
        [Required]
        [MaxLength(100)]
        public string LinkName { get; set; }

        [Display(Name = "連結路徑")]
        [Required]
        public string LinkPath { get; set; }

        [Display(Name = "相片路徑")]
        public string PhotoPath { get; set; }

        [Display(Name = "是否顯示標籤")]
        public bool? IsShow { get; set; }

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