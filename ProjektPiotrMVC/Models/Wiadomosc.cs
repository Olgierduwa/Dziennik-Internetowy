using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models
{
    public class Wiadomosc
    {
        public int WiadomoscID { get; set; }
        public string Tresc { get; set; }
        public DateTime DataDodania { get; set; }

        public string NadawcaID { get; set; }
        [ForeignKey("NadawcaID")]
        public virtual ApplicationUser Nadawca { get; set; }

        public string OdbiorcaID { get; set; }
        [ForeignKey("OdbiorcaID")]
        public virtual ApplicationUser Odbiorca { get; set; }
    }
}