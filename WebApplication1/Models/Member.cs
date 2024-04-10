using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//進行編輯操作時能夠看到適當格式的日期時間
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime? UpdateTime { get; set; }
    }
    //public class MemberCreateViewModel
    //{
    //    [Required(ErrorMessage = "{0}必填")]
    //    [MaxLength(400)]
    //    [Display(Name = "帳號")]
    //    public string Account { get; set; }

    //    [Required(ErrorMessage = "{0}必填")]
    //    [MaxLength(400)]
    //    [Display(Name = "密碼")]
    //    public string Password { get; set; }

    //    [Required(ErrorMessage = "{0}必填")]
    //    [MaxLength(100)]
    //    [Display(Name = "暱稱")]
    //    public string NickName { get; set; }

    //    [MaxLength(200)]
    //    [Display(Name = "權限")]
    //    public string Permission { get; set; }

    //}
}