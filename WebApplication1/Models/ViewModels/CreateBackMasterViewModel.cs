using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class CreateMasterProfileViewModel
    {
        [Display(Name = "姓名")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "職業")]
        [Required]
        [MaxLength(500)]
        public string Ocupation { get; set; }
    }

    public class CreateMasterPhotoViewModel
    {
        [Display(Name = "圖片名稱和路徑")]
        [Required]
        [MaxLength(500)]
        public string PhotoPath { get; set; }
    }
    public class EditMasterProfileViewModel
    {
        [Display(Name = "姓名")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Display(Name = "職業")]
        [Required]
        [MaxLength(500)]
        public string Ocupation { get; set; }

        [AllowHtml]
        [Display(Name = "CKeditor內容")]
        public string PersonCkContent { get; set; }

    }
}