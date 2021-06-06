using Microsoft.AspNet.Identity;
using ProjektPiotrMVC.Models;
using ProjektPiotrMVC.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektPiotrMVC.Controllers
{
    public class NauczycielController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Nauczyciel
        public ActionResult Index()
        {
            string UserID = HttpContext.User.Identity.GetUserId();
            Przedmiot przedmiot = db.Przedmiots.Where(m => m.ProwadzacyID == UserID).FirstOrDefault();
            List<Plik> Pliki = przedmiot.Pliki.ToList();
            List<Klasa> Klasy = new List<Klasa>();
            foreach (var klasy in przedmiot.KlasyPrzedmioty)
                Klasy.Add(db.Klasas.Find(klasy.KlasaID));
            ManagePrzedmiotViewModel mp = new ManagePrzedmiotViewModel()
            {
                PrzedmiotID = przedmiot.PrzedmiotID,
                Nazwa = przedmiot.Nazwa,
                Pliki = Pliki,
                Klasy = Klasy
            };
            return View(mp);
        }

        public ActionResult ManageClass(int KlasaID)
        {
            List<ApplicationUser> Uczniowie = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "e56a0720-73d4-4b8f-9ed5-15cb4d9a6b8f" && m.KlasaID == KlasaID).ToList();
            Klasa klasa = db.Klasas.Find(KlasaID);
            ViewBag.Klasa = klasa.Nazwa + "\" z rocznika " + klasa.Rocznik;
            ViewBag.KlasaID = KlasaID;
            return View(Uczniowie);
        }

    }
}