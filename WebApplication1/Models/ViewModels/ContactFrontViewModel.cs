using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class ContactFrontViewModel
    {
        [Required]
        [MaxLength(60)]
        public string name { get; set; }
        [Required(ErrorMessage = "尚未選擇性別")]
        public sex? gender { get; set; }
        [Required]
        public string phone { get; set; }
        [Required(ErrorMessage ="信箱格式不正確")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string email { get; set; }
        [Required]
        [MaxLength(500)]
        public string content { get; set; }
    }
    public enum sex
    {
        女 = 0,
        男 = 1,
    }
}