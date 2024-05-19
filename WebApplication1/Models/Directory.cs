using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("Directory")]
    public class Directory
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "目錄名稱")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "目錄代號")]
        public string Value { get; set; }

        [Display(Name = "是否為登入後顯示")]
        public LoginAuth? IsAuthMenu { get; set; }

        [Display(Name = "目錄迴圈編號")]
        public int? RecursiveId { get; set; } 

        [ForeignKey("RecursiveId")]
        [Display(Name = "權限父成員")]
        public virtual Directory ParentTable { get; set; }

        [Display(Name = "創立時間")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "權限子成員")]
        public virtual ICollection<Directory> ChildTable { get; set; }

        [Display(Name = "HTML內容表單成員")]
        public virtual ICollection<WebContent> WebContentTable { get; set; }
    }

    public enum LoginAuth 
    {
        Both=0,
        BeforeLogin=1,
        AfterLogin=2,
    }

}