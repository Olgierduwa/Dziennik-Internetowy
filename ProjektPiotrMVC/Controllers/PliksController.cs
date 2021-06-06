using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektPiotrMVC.Models;

namespace ProjektPiotrMVC.Controllers
{
    public class PliksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(int przedmiotID)
        {
            Plik plik = new Plik() { PrzedmiotID = przedmiotID, Format = ".pdf .txt .zip" };
            return View(plik);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Plik plikview)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(plikview.Plikozor.FileName);
                plikview.Lokalizacja = "~/Files/" + fileName;
                plikview.Plikozor.SaveAs(Path.Combine(Server.MapPath("~/Files"), fileName));
                string[] format = fileName.Split('.');

                Plik plik = new Plik
                {
                    Nazwa = plikview.Nazwa,
                    Lokalizacja = plikview.Lokalizacja,
                    Format = format.Last(),
                    Plikozor = plikview.Plikozor,
                    PrzedmiotID = plikview.PrzedmiotID
                };

                db.Pliks.Add(plik);
                db.SaveChanges();

                return RedirectToAction("Index", "Nauczyciel");
            }

            ViewBag.PrzedmiotID = new SelectList(db.Przedmiots, "PrzedmiotID", "Nazwa", plikview.PrzedmiotID);
            return View(plikview);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plik plik = db.Pliks.Find(id);
            if (plik == null)
            {
                return HttpNotFound();
            }
            return View(plik);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plik plik = db.Pliks.Find(id);
            db.Pliks.Remove(plik);
            db.SaveChanges();
            return RedirectToAction("Index", "Nauczyciel");
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
