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
    public class KlasasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Klasas
        public ActionResult Index()
        {
            List<Klasa> klasas = db.Klasas.Include(k => k.Wychowawca).ToList();
            foreach(var klasa in klasas)
                klasa.IloscUczniow = klasa.Uczniowie.Count;
            return View(klasas.ToList());
        }

        // GET: Klasas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klasa klasa = db.Klasas.Find(id);
            if (klasa == null)
            {
                return HttpNotFound();
            }
            return View(klasa);
        }

        // GET: Klasas/Create\
        public ActionResult Create()
        {
            var Nauczyciele = db.Users.Where(m=>m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870");
            var Wychowawcy = Nauczyciele.Where(m => m.KlasaID == 0);
            ViewBag.WychowawcaID = new SelectList(Wychowawcy, "Id", "Email");
            return View();
        }

        // POST: Klasas/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KlasaID,Nazwa,Rocznik,WychowawcaID")] Klasa klasa)
        {
            if (ModelState.IsValid && klasa.WychowawcaID != null)
            {
                db.Klasas.Add(klasa);
                db.SaveChanges();

                ApplicationUser Wychowawca = db.Users.Find(klasa.WychowawcaID);
                Wychowawca.KlasaID = klasa.KlasaID;
                db.Entry(Wychowawca).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var Nauczyciele = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870");
            var Wychowawcy = Nauczyciele.Where(m => m.KlasaID == 0);
            ViewBag.WychowawcaID = new SelectList(Wychowawcy, "Id", "Email", klasa.WychowawcaID);
            return View(klasa);
        }

        // GET: Klasas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klasa Klasa = db.Klasas.Find(id);
            EditClassViewModel ec = new EditClassViewModel()
            {
                KlasaID = Klasa.KlasaID,
                Nazwa = Klasa.Nazwa,
                Rocznik = Klasa.Rocznik,
                WychowawcaID = Klasa.WychowawcaID,
                StaryWychowawcaID = Klasa.WychowawcaID,
                Uczniowie = Klasa.Uczniowie
            };
            if (ec == null)
            {
                return HttpNotFound();
            }
            var Nauczyciele = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870");
            var Wychowawcy = Nauczyciele.Where(m => m.KlasaID == 0 || m.Id == ec.WychowawcaID);
            ViewBag.WychowawcaID = new SelectList(Wychowawcy, "Id", "Email", ec.WychowawcaID);
            return View(ec);
        }

        // POST: Klasas/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KlasaID,Nazwa,Rocznik,WychowawcaID,StaryWychowawcaID")] EditClassViewModel ec)
        {
            if (ModelState.IsValid)
            {
                Klasa klasa = db.Klasas.Find(ec.KlasaID);
                klasa.Nazwa = ec.Nazwa;
                klasa.Rocznik = ec.Rocznik;
                klasa.WychowawcaID = ec.WychowawcaID;

                db.Users.Find(ec.StaryWychowawcaID).KlasaID = 0;
                db.Users.Find(klasa.WychowawcaID).KlasaID = klasa.KlasaID;

                db.Entry(klasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var Nauczyciele = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870");
            var Wychowawcy = Nauczyciele.Where(m => m.KlasaID == 0 || m.Id == ec.WychowawcaID);
            ViewBag.WychowawcaID = new SelectList(Wychowawcy, "Id", "Email", ec.WychowawcaID);
            return View(ec);
        }

        public ActionResult AddToClass(int? KlasaID)
        {
            AddToClassViewModel atc = new AddToClassViewModel();
            atc.KlasaID = KlasaID;

            var Uczniowie = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "e56a0720-73d4-4b8f-9ed5-15cb4d9a6b8f");
            var Dostepni = Uczniowie.Where(m => m.KlasaID == 0);
            ViewBag.UczenID = new SelectList(Dostepni, "Id", "Email");
            return View(atc);
        }

        [HttpPost]
        public ActionResult AddToClass([Bind(Include = "KlasaID,UczenID")] AddToClassViewModel atc)
        {
            if (ModelState.IsValid && atc.UczenID != null && atc.KlasaID != null)
            {
                Klasa klasa = db.Klasas.Where(m => m.KlasaID == atc.KlasaID).FirstOrDefault();
                klasa.Uczniowie.Add(db.Users.Where(m => m.Id == atc.UczenID).FirstOrDefault());
                db.Users.Where(m => m.Id == atc.UczenID).FirstOrDefault().KlasaID = Convert.ToInt32(atc.KlasaID);
                db.Entry(klasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = atc.KlasaID });
            }
            var Uczniowie = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == "e56a0720-73d4-4b8f-9ed5-15cb4d9a6b8f");
            List<ApplicationUser> Dostepni = Uczniowie.Where(m => m.KlasaID == 0).ToList();
            ViewBag.UczenID = new SelectList(Dostepni, "Id", "Email");
            return View(atc);
        }

        public ActionResult DelFromClass(int KlasaID, string UczenID)
        {
            if (UczenID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DelFromClassViewModel dfc = new DelFromClassViewModel();
            ApplicationUser User =  db.Users.Where(m => m.Id == UczenID).FirstOrDefault();
            dfc.Uczen = User;
            dfc.KlasaID = KlasaID;
            dfc.UczenID = User.Id;
            if (dfc == null)
            {
                return HttpNotFound();
            }
            return View(dfc);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("DelFromClass")]
        [ValidateAntiForgeryToken]
        public ActionResult DelFromClassConfirmed([Bind(Include = "KlasaID,UczenID,Uczen")] DelFromClassViewModel dfc)
        {
            Klasa klasa = db.Klasas.Where(m => m.KlasaID == dfc.KlasaID).FirstOrDefault();
            ApplicationUser Uczen = db.Users.Where(m => m.Id == dfc.UczenID).First();
            klasa.Uczniowie.Remove(Uczen);
            db.Users.Where(m => m.Id == dfc.UczenID).FirstOrDefault().KlasaID = 0;
            db.Entry(klasa).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = dfc.KlasaID });
        }

        // GET: Klasas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klasa klasa = db.Klasas.Find(id);
            if (klasa == null)
            {
                return HttpNotFound();
            }
            return View(klasa);
        }

        // POST: Klasas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klasa klasa = db.Klasas.Find(id);
            db.Users.Find(klasa.WychowawcaID).KlasaID = 0;

            foreach (var uczen in klasa.Uczniowie)
                db.Users.Find(uczen.Id).KlasaID = 0;

            List<KlasaPrzedmiot> klasyprzedmioty = new List<KlasaPrzedmiot>();
            klasyprzedmioty.AddRange(klasa.KlasyPrzedmioty);
            foreach(var przedmiot in klasyprzedmioty)
            {
                db.KlasaPrzedmiots.Remove(przedmiot);
                //db.Przedmiots.Find(przedmiot.PrzedmiotID).KlasyPrzedmioty.Remove(przedmiot);
            }

            db.Klasas.Remove(klasa);
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
