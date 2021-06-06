using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models.ViewsModels
{
    public class DelFromPrzedmiotViewModel
    {
        public int PrzedmiotID { get; set; }
        public int KlasaID { get; set; }
        public Klasa Klasa { get; set; }
    }

    public class DelFromClassViewModel
    {
        public int? KlasaID { get; set; }
        public string UczenID { get; set; }
        public ApplicationUser Uczen { get; set; }
    }

    public class DelFromRodzicViewModel
    {
        public string RodzicID { get; set; }
        public string UczenID { get; set; }
        public ApplicationUser Rodzic { get; set; }
        public ApplicationUser Uczen { get; set; }
    }

}