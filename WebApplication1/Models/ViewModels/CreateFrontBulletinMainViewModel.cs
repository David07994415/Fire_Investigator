using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models.ViewModels
{
    public class CreateFrontBulletinMainViewModel
    {
    }

    public class CreateBulletinMain
    {
        [Required]
        [DisplayName("主題名稱")]
        public string theme {  get; set; }

        [AllowHtml]
        [Required]
        [DisplayName("主題內容")]
        public string CkConent { get; set; }
    }

}