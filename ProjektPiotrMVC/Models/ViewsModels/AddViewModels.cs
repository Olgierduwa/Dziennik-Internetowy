using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models.ViewsModels
{
    public class AddToClassViewModel
    {
        public int? KlasaID { get; set; }
        public string UczenID { get; set; }
    }

    public class AddToRodzicViewModel
    {
        public string RodzicID { get; set; }
        public string UczenID { get; set; }
    }

    public class AddToPrzedmiotViewModel
    {
        public int? PrzedmiotID { get; set; }
        public int KlasaID { get; set; }
    }
}