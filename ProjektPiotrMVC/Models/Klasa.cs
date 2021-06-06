using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models
{
    public class Klasa
    {
        public int KlasaID { get; set; }
        [Required]
        public string Nazwa { get; set; }
        [Required]
        public int Rocznik { get; set; }
        [NotMapped]
        [Display(Name = "Ilość uczniów")]
        public int IloscUczniow { get; set; }

        public string WychowawcaID { get; set; }
        [ForeignKey("WychowawcaID")]
        public virtual ApplicationUser Wychowawca { get; set; }

        public virtual ICollection<ApplicationUser> Uczniowie { get; set; }
        public virtual ICollection<KlasaPrzedmiot> KlasyPrzedmioty { get; set; }
    }
}