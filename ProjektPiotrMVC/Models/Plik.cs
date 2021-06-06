using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models
{
    public class Plik
    {
        public int PlikID { get; set; }
        [Required]
        public string Nazwa { get; set; }
        [Display(Name = "Formaty")]
        public string Format { get; set; }
        [Display(Name = "Wybierz plik")]
        public string Lokalizacja { get; set; }


        [NotMapped]
        [Required]
        public HttpPostedFileBase Plikozor { get; set; }

        public int PrzedmiotID { get; set; }
        [ForeignKey("PrzedmiotID")]
        public virtual Przedmiot Przedmiot { get; set; }
    }
}