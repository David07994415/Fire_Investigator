using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class CreateBackBusinessViewModel
    {
    }
    public class EditBackBusinessViewModel
    {
        [Required]
        [AllowHtml]
        [Display(Name = "CKeditor內容")]
        public string Content { get; set; }
    }
}