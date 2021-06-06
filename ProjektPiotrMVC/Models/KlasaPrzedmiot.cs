using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models
{
    public class KlasaPrzedmiot
    {
        public int KlasaPrzedmiotID { get; set; }

        public int KlasaID { get; set; }
        [ForeignKey("KlasaID")]
        public virtual Klasa Klasa { get; set; }

        public int PrzedmiotID { get; set; }
        [ForeignKey("PrzedmiotID")]
        public virtual Przedmiot Przedmiot { get; set; }
    }
}