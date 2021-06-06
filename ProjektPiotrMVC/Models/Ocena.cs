using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models
{
    public class Ocena
    {
        public int OcenaID { get; set; }
        public string Kategoria { get; set; }
        public double Wartosc { get; set; }
        [Display(Name = "Data wystawienia")]
        public DateTime DataDodania { get; set; }

        public string UczenID { get; set; }
        [ForeignKey("UczenID")]
        public virtual ApplicationUser Uczen { get; set; }

        public int PrzedmiotID { get; set; }
        [ForeignKey("PrzedmiotID")]
        public virtual Przedmiot Przedmiot { get; set; }
    }
}