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
        [Required]
        public sex gender { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string email { get; set; }
        [Required]
        [MaxLength(500)]
        public string content { get; set; }
        [Required]
        public string validationCode { get; set; }
    }
    public enum sex
    {
        女 = 0,
        男 = 1,
    }
}