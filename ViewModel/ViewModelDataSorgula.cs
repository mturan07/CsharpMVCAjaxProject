using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Data;

namespace WebApp.ViewModel
{
    public class ViewModelDataSorgula
    {
        public List<SelectListItem> Sehirler { get; set; }
        public List<SelectListItem> Ilceler { get; set; }
        public List<SelectListItem> Mahalleler { get; set; }
    }
}