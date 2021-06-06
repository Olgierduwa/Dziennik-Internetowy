using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models
{
    public class Ogloszenie
    {
        public int OgloszenieID { get; set; }
        [Required]
        public string Tytul { get; set; }
        [Required]
        public string Kontent { get; set; }
        [Display(Name = "Data Dodania")]
        public DateTime DataDodania { get; set; }
        public bool Prywatne { get; set; }

        public string AutorID { get; set; }
        [ForeignKey("AutorID")]
        public virtual ApplicationUser Autor { get; set; }
    }
}