﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("Master")]
    public class Master
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "職業")]
        [Required]
        [MaxLength(500)]
        public string Ocupation { get; set; }

        [Display(Name = "相片路徑")]
        public string PhotoPath { get; set; }

        [Display(Name = "CKeditor內容")]
        public string PersonCkContent { get; set; }

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