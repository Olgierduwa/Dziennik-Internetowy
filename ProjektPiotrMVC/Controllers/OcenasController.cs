using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProjektPiotrMVC.Models;

namespace ProjektPiotrMVC.Controllers
{
    public class OcenasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ocenas
        public ActionResult IndexUczen()
        {
            string UczenID = HttpContext.User.Identity.GetUserId();
            var ocenas = db.Ocenas.Where(m => m.UczenID == UczenID);
            ApplicationUser user = db.Users.Find(UczenID);
            ViewBag.Uczen = user.Imie + " " + user.Nazwisko;
            return View(ocenas.ToList());
        }

        public ActionResult IndexRodzic(string UczenID, string RodzicID)
        {
            var ocenas = db.Ocenas.Where(m=>m.UczenID == UczenID);
            ApplicationUser user = db.Users.Find(UczenID);
            ViewBag.Uczen = user.Imie + " " + user.Nazwisko;
            ViewBag.RodzicID = RodzicID;
            return View(ocenas.ToList());
        }

        [Authorize(Roles = "Nauczyciel")]
        public ActionResult IndexNauczyciel(string UczenID, int KlasaID)
        {
            string ProwadzacyID = HttpContext.User.Identity.GetUserId();
            var ocenas = db.Ocenas.Where(m => m.UczenID == UczenID && m.Przedmiot.ProwadzacyID == ProwadzacyID);
            ApplicationUser user = db.Users.Find(UczenID);
            ViewBag.Uczen = user.Imie + " " + user.Nazwisko;
            ViewBag.UczenID = UczenID;
            ViewBag.KlasaID = KlasaID;
            return View(ocenas.ToList());
        }

        // GET: Ocenas/Create
        [Authorize(Roles = "Nauczyciel")]
        public ActionResult Create(string UczenID, int KlasaID)
        {
            List<SelectListItem> oceny = new List<SelectListItem>();
            oceny.Add(new SelectListItem { Text = "2", Value = "2" });
            oceny.Add(new SelectListItem { Text = "3", Value = "3" });
            oceny.Add(new SelectListItem { Text = "3,5", Value = "3,5" });
            oceny.Add(new SelectListItem { Text = "4", Value = "4" });
            oceny.Add(new SelectListItem { Text = "4,5", Value = "4,5" });
            oceny.Add(new SelectListItem { Text = "5", Value = "5" });

            List<SelectListItem> kategorie = new List<SelectListItem>();
            kategorie.Add(new SelectListItem { Text = "Sprawdzian", Value = "Sprawdzian" });
            kategorie.Add(new SelectListItem { Text = "Kartkówka", Value = "Kartkówka" });
            kategorie.Add(new SelectListItem { Text = "Konkurs", Value = "Konkurs" });
            kategorie.Add(new SelectListItem { Text = "Praca domowa", Value = "Praca domowa" });
            kategorie.Add(new SelectListItem { Text = "Praca w grupie", Value = "Praca w grupie" });
            kategorie.Add(new SelectListItem { Text = "Praca dodatkowa", Value = "Praca dodatkowa" });

            ViewBag.Wartosc = new SelectList(oceny, "Text", "Value");
            ViewBag.Kategoria = new SelectList(kategorie, "Text", "Value");
            ViewBag.KlasaID = KlasaID;

            string UserID = HttpContext.User.Identity.GetUserId();
            int PrzedmiotID = db.Users.Find(UserID).PrzedmiotID;

            Ocena ocena = new Ocena() { UczenID = UczenID, OcenaID = KlasaID, PrzedmiotID = PrzedmiotID };
            return View(ocena);
        }

        // POST: Ocenas/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Nauczyciel")]
        public ActionResult Create(Ocena ocena)
        {
            int KlasaID = ocena.OcenaID;
            if (ModelState.IsValid)
            {
                ocena.OcenaID = 0;
                Ocena newocena = ocena;
                newocena.DataDodania = DateTime.Now;
                db.Ocenas.Add(ocena);
                db.SaveChanges();
                return RedirectToAction("IndexNauczyciel", "Ocenas", new { UczenID = ocena.UczenID, KlasaID = KlasaID });
            }
            return View(ocena);
        }

        // GET: Ocenas/Delete/5
        [Authorize(Roles = "Nauczyciel")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocena ocena = db.Ocenas.Find(id);
            if (ocena == null)
            {
                return HttpNotFound();
            }
            return View(ocena);
        }

        // POST: Ocenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Nauczyciel")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ocena ocena = db.Ocenas.Find(id);
            ApplicationUser user = db.Users.Find(ocena.UczenID);
            db.Ocenas.Remove(ocena);
            db.SaveChanges();
            return RedirectToAction("IndexNauczyciel", "Ocenas", new { UczenID = user.Id, KlasaID = user.KlasaID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
