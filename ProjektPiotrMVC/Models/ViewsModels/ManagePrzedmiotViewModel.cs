using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models.ViewsModels
{
    public class ManagePrzedmiotViewModel
    {
        public int PrzedmiotID { get; set; }
        public string ProwadzacyID { get; set; }
        public string Nazwa { get; set; }

        public virtual ICollection<Plik> Pliki { get; set; }
        public virtual ICollection<Klasa> Klasy { get; set; }
        public Klasa Klasa { get; set; }
        public Plik Plik { get; set; }
    }
}