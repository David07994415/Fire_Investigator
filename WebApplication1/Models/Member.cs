using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace WebApplication1.Models
{
    [Table("Member")]
    public class Member
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(400)]
        [Display(Name = "密碼鹽")]
        public string Salt { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "暱稱")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "guid")]
        public Guid Guid { get; set; }

        [MaxLength(200)]
        [Display(Name = "權限")]
        public string Permission { get; set; }

        [Display(Name = "創建資料之時間")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreateTime { get; set; }

        [Display(Name = "更新資料之時間")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime? UpdateTime { get; set; }

        [Display(Name = "更新資料之使用者")]
        public int? UpdateUser { get; set; }

        [Display(Name = "創建資料之使用者")]
        public int? CreateUser { get; set; }

        [Display(Name = "是否審核通過")]
        public bool? IsApproved { get; set; }

        [Display(Name = "帳號身分類別")]
        public IdentityCategory? IdCat { get; set; }
    }

    public enum IdentityCategory
    {
        [Display(Name = "Both Sides")]
        Both =0,

        [Display(Name = "Front End Only")]
        FrontOnly =1,

        [Display(Name = "Back End Only")]
        BackOnly =2
    }
    
}