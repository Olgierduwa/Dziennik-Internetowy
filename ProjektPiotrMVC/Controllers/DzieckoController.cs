using Microsoft.AspNet.Identity;
using ProjektPiotrMVC.Models;
using ProjektPiotrMVC.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjektPiotrMVC.Controllers
{
    public class DzieckoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Dziecko
        public ActionResult Index(string id)
        {
            List<UczenRodzic> urs = db.UczenRodzics.Where(m => m.RodzicID == id).ToList();
            List<UczenRodzic> model = new List<UczenRodzic>();
            ApplicationUser user = db.Users.Find(id);
            var Klasy = db.Klasas;
            foreach (var item in urs)
            {
                item.Uczen.Klasa = Klasy.Find(item.Uczen.KlasaID);
            }
            ViewBag.Rodzic = user.Imie + " " + user.Nazwisko;
            ViewBag.RodzicID = user.Id;
            return View(urs);
        }
        
        public ActionResult IndexRodzic()
        {
            string RodzicID = HttpContext.User.Identity.GetUserId();
            List<UczenRodzic> urs = db.UczenRodzics.Where(m => m.RodzicID == RodzicID).ToList();
            List<UczenRodzic> model = new List<UczenRodzic>();
            ApplicationUser user = db.Users.Find(RodzicID);
            var Klasy = db.Klasas;
            foreach (var item in urs)
            {
                item.Uczen.Klasa = Klasy.Find(item.Uczen.KlasaID);
            }
            return View(urs);
        }

        public ActionResult Add(string RodzicID)
        {
            AddToRodzicViewModel atr = new AddToRodzicViewModel();
            atr.RodzicID = RodzicID;
            List<ApplicationUser> Uczniowie = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "e56a0720-73d4-4b8f-9ed5-15cb4d9a6b8f").ToList();
            List<ApplicationUser> Dostepni = new List<ApplicationUser>();
            foreach (var uczen in Uczniowie)
            {
                uczen.UczniowieRodzice = db.UczenRodzics.Where(m => m.UczenID == uczen.Id).ToList();
                bool sprawa = true;
                foreach (var uczniorodzice in uczen.UczniowieRodzice)
                    if (uczniorodzice.RodzicID == atr.RodzicID) sprawa = false;

                if (sprawa)
                {
                    ApplicationUser dostepny = db.Users.Find(uczen.Id);
                    Dostepni.Add(dostepny);
                }
            }
            ViewBag.UczenID = new SelectList(Dostepni, "Id", "Email");
            return View(atr);
        }
        [HttpPost]
        public ActionResult Add(AddToRodzicViewModel atr)
        {
            if (ModelState.IsValid && atr.UczenID != null)
            {
                UczenRodzic uczenRodzic = new UczenRodzic()
                {
                    UczenID = atr.UczenID,
                    RodzicID = atr.RodzicID
                };
                db.UczenRodzics.Add(uczenRodzic);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = atr.RodzicID });
            }
            List<ApplicationUser> Uczniowie = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "e56a0720-73d4-4b8f-9ed5-15cb4d9a6b8f").ToList();
            List<ApplicationUser> Dostepni = new List<ApplicationUser>();
            foreach (var uczen in Uczniowie)
            {
                uczen.UczniowieRodzice = db.UczenRodzics.Where(m => m.UczenID == uczen.Id).ToList();
                bool sprawa = true;
                foreach (var uczniorodzice in uczen.UczniowieRodzice)
                    if (uczniorodzice.RodzicID == atr.RodzicID) sprawa = false;

                if (sprawa)
                {
                    ApplicationUser dostepny = db.Users.Find(uczen.Id);
                    Dostepni.Add(dostepny);
                }
            }
            ViewBag.UczenID = new SelectList(Dostepni, "Id", "Email");
            return View(atr);
        }

        public ActionResult Delete(string RodzicID, string UczenID)
        {
            if (UczenID == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DelFromRodzicViewModel dfr = new DelFromRodzicViewModel();
            ApplicationUser User = db.Users.Find(UczenID);
            ApplicationUser Rodzic = db.Users.Find(RodzicID);
            dfr.Uczen = User;
            dfr.Rodzic = Rodzic;
            dfr.Uczen.Imie = User.Imie + " " + User.Nazwisko;
            dfr.Rodzic.Imie = Rodzic.Imie + " " + Rodzic.Nazwisko;
            dfr.RodzicID = RodzicID;
            dfr.UczenID = UczenID;
            if (dfr == null)
            {
                return HttpNotFound();
            }
            return View(dfr);
        }

        [HttpPost]
        public ActionResult Delete(DelFromRodzicViewModel dfr)
        {
            UczenRodzic uczenRodzic = db.UczenRodzics.Where(m => m.RodzicID == dfr.RodzicID && m.UczenID == dfr.UczenID).FirstOrDefault();
            db.UczenRodzics.Remove(uczenRodzic);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = dfr.RodzicID });
        }

    }
}