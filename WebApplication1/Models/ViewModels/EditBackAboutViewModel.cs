using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class EditBackAboutViewModel
    {
        [AllowHtml]
        [Display(Name = "CKeditor內容")]
        [Required]
        public string HTMLContent { get; set; }
    }
}