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
        public string HTMLContent { get; set; }
    }
}