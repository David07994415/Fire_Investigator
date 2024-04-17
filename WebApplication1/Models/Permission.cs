using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Security;

namespace WebApplication1.Models
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "表單名稱")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "表單代號")]
        public string Value { get; set; }

        [MaxLength(400)]
        [Display(Name = "路由代號")]
        public string URL { get; set; }

        [Display(Name = "權限迴圈編號")]
        public int? RecursiveId { get; set; } //可以沒有
        [ForeignKey("RecursiveId")]
        [Display(Name = "權限父成員")]
        public virtual Permission ParentTable { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應


        [Display(Name = "創立時間")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "權限子成員")]
        public virtual ICollection<Permission> ChildTable { get; set; }
    }
}