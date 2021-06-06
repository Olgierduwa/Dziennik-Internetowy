using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models
{
    public class Przedmiot
    {
        public int PrzedmiotID { get; set; }
        [Required]
        [Display(Name = "Nazwa przedmiotu")]
        public string Nazwa { get; set; }
        [NotMapped]
        [Display(Name = "Ilość Klas")]
        public int IloscKlas { get; set; }

        [Required]
        public string ProwadzacyID { get; set; }
        [ForeignKey("ProwadzacyID")]
        public virtual ApplicationUser Prowadzacy { get; set; }

        public virtual ICollection<KlasaPrzedmiot> KlasyPrzedmioty { get; set; }
        public virtual ICollection<Plik> Pliki { get; set; }
    }
}