using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjektPiotrMVC.Models
{
    public class UczenRodzic
    {
        public int UczenRodzicID { get; set; }

        public string UczenID { get; set; }
        [ForeignKey("UczenID")]
        public virtual ApplicationUser Uczen { get; set; }

        public string RodzicID { get; set; }
        [ForeignKey("RodzicID")]
        public virtual ApplicationUser Rodzic { get; set; }
    }
}