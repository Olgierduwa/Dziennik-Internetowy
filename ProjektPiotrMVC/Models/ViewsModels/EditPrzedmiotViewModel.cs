using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models.ViewsModels
{
    public class EditPrzedmiotViewModel
    {
        public int PrzedmiotID { get; set; }
        [Required]
        public string Nazwa { get; set; }

        public string StaryProwadzacyID { get; set; }
        public string ProwadzacyID { get; set; }
        [ForeignKey("ProwadzacyID")]
        public virtual ApplicationUser Prowadzacy { get; set; }

        public virtual ICollection<Klasa> Klasy { get; set; }
        public Klasa Klasa { get; set; }
    }
}