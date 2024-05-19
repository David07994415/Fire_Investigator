using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ViewModels
{
    public class HomeDataViewModel
    {
        public List<IndexCover> CoverData { get; set; }
        public  List<News> NewsData { get; set; }
        public IndexPurpose PurposeData { get; set; }
        public List<IndexLink> LinkData { get; set; }
    }
}