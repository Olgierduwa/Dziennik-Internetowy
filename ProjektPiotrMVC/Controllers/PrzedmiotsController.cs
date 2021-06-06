using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektPiotrMVC.Models;
using ProjektPiotrMVC.Models.ViewsModels;

namespace ProjektPiotrMVC.Controllers
{
    public class PrzedmiotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Przedmiots
        public ActionResult Index()
        {
            List<Przedmiot> przedmiots = db.Przedmiots.ToList();
            foreach (var przedmiot in przedmiots)
                przedmiot.IloscKlas = przedmiot.KlasyPrzedmioty.Count;
            return View(przedmiots.ToList());
        }

        // GET: Przedmiots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmiots.Find(id);
            List<Klasa> Klasy = new List<Klasa>();
            foreach (var klasy in przedmiot.KlasyPrzedmioty)
            {
                Klasa Klasa = db.Klasas.Find(klasy.KlasaID);
                Klasy.Add(Klasa);
            }
            EditPrzedmiotViewModel ep = new EditPrzedmiotViewModel()
            {
                PrzedmiotID = przedmiot.PrzedmiotID,
                Nazwa = przedmiot.Nazwa,
                ProwadzacyID = przedmiot.ProwadzacyID,
                Prowadzacy = przedmiot.Prowadzacy,
                StaryProwadzacyID = przedmiot.ProwadzacyID,
                Klasy = Klasy
            };
            if (ep == null)
            {
                return HttpNotFound();
            }
            return View(ep);
        }

        // GET: Przedmiots/Create
        public ActionResult Create()
        {
            var Nauczyciele = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870");
            var Prowadzacy = Nauczyciele.Where(m => m.PrzedmiotID == 0);
            ViewBag.ProwadzacyID = new SelectList(Prowadzacy, "Id", "Email");
            return View();
        }

        // POST: Przedmiots/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrzedmiotID,Nazwa,ProwadzacyID")] Przedmiot przedmiot)
        {
            if (ModelState.IsValid)
            {
                db.Przedmiots.Add(przedmiot);
                db.SaveChanges();

                ApplicationUser prowadzacy = db.Users.Find(przedmiot.ProwadzacyID);
                prowadzacy.PrzedmiotID = przedmiot.PrzedmiotID;
                db.Entry(prowadzacy).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var Nauczyciele = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870");
            var Prowadzacy = Nauczyciele.Where(m => m.PrzedmiotID == 0);
            ViewBag.ProwadzacyID = new SelectList(Prowadzacy, "Id", "Email");
            return View(przedmiot);
        }

        // GET: Przedmiots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot Przedmiot = db.Przedmiots.Find(id);
            List<Klasa> Klasy = new List<Klasa>();
            foreach(var klasy in Przedmiot.KlasyPrzedmioty)
            {
                Klasa Klasa = db.Klasas.Find(klasy.KlasaID);
                Klasy.Add(Klasa);
            }
            EditPrzedmiotViewModel ep = new EditPrzedmiotViewModel()
            {
                PrzedmiotID = Przedmiot.PrzedmiotID,
                Nazwa = Przedmiot.Nazwa,
                ProwadzacyID = Przedmiot.ProwadzacyID,
                StaryProwadzacyID = Przedmiot.ProwadzacyID,
                Klasy = Klasy
            };
            if (ep == null)
            {
                return HttpNotFound();
            }
            var Nauczyciele = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870");
            var Prowadzacy = Nauczyciele.Where(m => m.PrzedmiotID == 0 || m.Id == ep.ProwadzacyID);
            ViewBag.ProwadzacyID = new SelectList(Prowadzacy, "Id", "Email", ep.ProwadzacyID);
            return View(ep);
        }

        // POST: Przedmiots/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrzedmiotID,Nazwa,ProwadzacyID,StaryProwadzacyID")] EditPrzedmiotViewModel ep)
        {
            if (ModelState.IsValid)
            {
                Przedmiot przedmiot = db.Przedmiots.Find(ep.PrzedmiotID);
                przedmiot.Nazwa = ep.Nazwa;
                przedmiot.ProwadzacyID = ep.ProwadzacyID;

                db.Users.Find(ep.StaryProwadzacyID).PrzedmiotID = 0;
                db.Users.Find(przedmiot.ProwadzacyID).PrzedmiotID = przedmiot.PrzedmiotID;

                db.Entry(przedmiot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var Nauczyciele = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870");
            var Prowadzacy = Nauczyciele.Where(m => m.PrzedmiotID == 0 || m.Id == ep.ProwadzacyID);
            ViewBag.ProwadzacyID = new SelectList(Prowadzacy, "Id", "Email", ep.ProwadzacyID);
            return View(ep);
        }

        public ActionResult AddToPrzedmiot(int? PrzedmiotID)
        {
            AddToPrzedmiotViewModel atp = new AddToPrzedmiotViewModel();
            atp.PrzedmiotID = PrzedmiotID;

            List<Klasa> Klasy = db.Klasas.ToList();
            List<Klasa> Dostepne = new List<Klasa>();
            foreach (var klasa in Klasy)
            {
                bool sprawa = true;
                foreach (var przedmiot in klasa.KlasyPrzedmioty)
                    if (przedmiot.PrzedmiotID == atp.PrzedmiotID) sprawa = false;

                if(sprawa)
                {
                    Klasa dostepna = db.Klasas.Find(klasa.KlasaID);
                    dostepna.Nazwa = dostepna.Rocznik.ToString() + " - " + dostepna.Nazwa;
                    Dostepne.Add(dostepna);
                }
            }
            ViewBag.KlasaID = new SelectList(Dostepne, "KlasaID", "Nazwa");
            return View(atp);
        }

        [HttpPost]
        public ActionResult AddToPrzedmiot([Bind(Include = "PrzedmiotID,KlasaID")] AddToPrzedmiotViewModel atp)
        {
            if (ModelState.IsValid && atp.PrzedmiotID != null && atp.KlasaID != 0)
            {
                KlasaPrzedmiot przedmiotklasa = new KlasaPrzedmiot()
                {
                    PrzedmiotID = Convert.ToInt32(atp.PrzedmiotID),
                    KlasaID = atp.KlasaID
                };
                db.KlasaPrzedmiots.Add(przedmiotklasa);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = Convert.ToInt32(atp.PrzedmiotID) });
            }
            List<Klasa> Klasy = db.Klasas.ToList();
            List<Klasa> Dostepne = new List<Klasa>();
            foreach (var klasa in Klasy)
            {
                bool sprawa = true;
                foreach (var przedmiot in klasa.KlasyPrzedmioty)
                    if (przedmiot.PrzedmiotID == atp.PrzedmiotID) sprawa = false;

                if (sprawa)
                {
                    Klasa dostepna = db.Klasas.Find(klasa.KlasaID);
                    dostepna.Nazwa = dostepna.Rocznik.ToString() + " - " + dostepna.Nazwa;
                    Dostepne.Add(dostepna);
                }
            }
            ViewBag.KlasaID = new SelectList(Dostepne, "KlasaID", "Nazwa");
            return View(atp);
        }

        public ActionResult DelFromPrzedmiot(int PrzedmiotID, int KlasaID)
        {
            if (KlasaID == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DelFromPrzedmiotViewModel dfc = new DelFromPrzedmiotViewModel();
            Klasa Klasa = db.Klasas.Find(KlasaID);
            dfc.Klasa = Klasa;
            dfc.Klasa.Nazwa = Klasa.Nazwa + "\"" + " z rocznika " + Klasa.Rocznik.ToString();
            dfc.PrzedmiotID = PrzedmiotID;
            dfc.KlasaID = KlasaID;
            if (dfc == null)
            {
                return HttpNotFound();
            }
            return View(dfc);
        }
        [HttpPost]
        public ActionResult DelFromPrzedmiot([Bind(Include = "PrzedmiotID,KlasaID")] DelFromPrzedmiotViewModel dfc)
        {
            KlasaPrzedmiot KlasaPrzedmiot = db.KlasaPrzedmiots.Where(m => m.PrzedmiotID == dfc.PrzedmiotID && m.KlasaID == dfc.KlasaID).FirstOrDefault();
            db.KlasaPrzedmiots.Remove(KlasaPrzedmiot);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = dfc.PrzedmiotID });
        }

        // GET: Przedmiots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przedmiot przedmiot = db.Przedmiots.Find(id);
            if (przedmiot == null)
            {
                return HttpNotFound();
            }
            return View(przedmiot);
        }

        // POST: Przedmiots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Przedmiot przedmiot = db.Przedmiots.Find(id);
            List<KlasaPrzedmiot> KlasyPrzedmioty = db.KlasaPrzedmiots.Where(m => m.PrzedmiotID == przedmiot.PrzedmiotID).ToList();

            db.Users.Find(przedmiot.ProwadzacyID).PrzedmiotID = 0;

            foreach(var item in KlasyPrzedmioty)
            {
                db.KlasaPrzedmiots.Remove(item);
                db.SaveChanges();
            }

            db.Przedmiots.Remove(przedmiot);
            db.SaveChanges();
            return RedirectToAction("Index");
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
