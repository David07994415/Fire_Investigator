using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("BusinessCategory")]
    public class BusinessCategory
    {
        [Key]                                  //主鍵PK
        [Display(Name = "編號")]  //呈現欄位的名稱
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //識別規格
        public int Id { get; set; }

        [Required]
        [Display(Name = "類型名稱")]
        [MaxLength(100)]
        public string Name { get; set; }


        [Display(Name = "更新資料之使用者")]
        public int UpdateUser { get; set; }


        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]//進行編輯操作時能夠看到適當格式的日期時間
        //[DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        [Display(Name = "更新資料之時間")]
        public DateTime? UpdateTime { get; set; }


        [Display(Name = "創建資料之使用者")]
        public int CreateUser { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "創建資料之時間")]
        public DateTime? CreateTime { get; set; }


        [Display(Name = "商業表單成員")]
        public virtual ICollection<Business> BussinessTable { get; set; }


    }
}