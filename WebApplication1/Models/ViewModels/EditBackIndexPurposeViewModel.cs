using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class EditBackIndexPurposeViewModel
    {
        [Required]
        [AllowHtml]
        [Display(Name ="CKediotor內容")]
        public string HTMLContent { get; set; }
    }
}