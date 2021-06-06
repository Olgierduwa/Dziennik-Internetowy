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
    public class OgloszeniesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ogloszenies
        public ActionResult Index()
        {
            string UserID = HttpContext.User.Identity.GetUserId();
            Klasa klasa = db.Klasas.Where(m=>m.WychowawcaID == UserID).FirstOrDefault();
            if(klasa == null) return RedirectToAction("BrakWychowawstwa");
            ViewBag.Klasa = klasa.Nazwa + " w roczniku " + klasa.Rocznik.ToString();
            var ogloszenies = db.Ogloszenies.Where(m=>m.Prywatne == true);
            return View(ogloszenies.ToList());
        }

        public ActionResult BrakWychowawstwa()
        {
            return View();
        }

        public ActionResult IndexUczen()
        {
            string UserID = HttpContext.User.Identity.GetUserId();
            ApplicationUser uczen = db.Users.Find(UserID);
            int KlasaID = uczen.KlasaID;
            string WychowawcaID = db.Users.Where(m => m.KlasaID == KlasaID && m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870").FirstOrDefault().Id;

            var ogloszenies = db.Ogloszenies.Where(m => m.Prywatne == true && m.AutorID == WychowawcaID);
            return View(ogloszenies.ToList());
        }

        public ActionResult IndexRodzic()
        {
            string RodzicID = HttpContext.User.Identity.GetUserId();
            ApplicationUser rodzic = db.Users.Find(RodzicID);
            List<UczenRodzic> rodzicdzieci = db.UczenRodzics.Where(m => m.RodzicID == RodzicID).ToList();
            List<Ogloszenie> ogloszenies = new List<Ogloszenie>();
            foreach (var rodzicdziecko in rodzicdzieci)
            {
                ApplicationUser dziecko = db.Users.Find(rodzicdziecko.UczenID);
                int KlasaID = dziecko.KlasaID;
                string WychowawcaID = db.Users.Where(m => m.KlasaID == KlasaID && m.Roles.FirstOrDefault().RoleId == "f073efe4-08eb-4b30-861a-dac97b61c870").FirstOrDefault().Id;
                List<Ogloszenie> ogloszenia = db.Ogloszenies.Where(m => m.Prywatne == true && m.AutorID == WychowawcaID).ToList();
                foreach(var ogloszenie in ogloszenia)
                {
                    if(!ogloszenies.Contains(ogloszenie))
                        ogloszenies.Add(ogloszenie);
                }
            }
            return View(ogloszenies);
        }


        // GET: Ogloszenies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = db.Ogloszenies.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }

        public ActionResult JustDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = db.Ogloszenies.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }

        // GET: Ogloszenies/Create
        public ActionResult Create(bool prywatne)
        {
            Ogloszenie ogloszenie = new Ogloszenie() { Prywatne = prywatne };
            return View(ogloszenie);
        }

        // POST: Ogloszenies/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                string UserID = HttpContext.User.Identity.GetUserId();
                ogloszenie.AutorID = UserID;
                ogloszenie.DataDodania = DateTime.Now;

                db.Ogloszenies.Add(ogloszenie);
                db.SaveChanges();
                if(ogloszenie.Prywatne)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", "Home");
            }

            return View(ogloszenie);
        }

        // GET: Ogloszenies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = db.Ogloszenies.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }

        // POST: Ogloszenies/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogloszenie).State = EntityState.Modified;
                db.SaveChanges();
                if (ogloszenie.Prywatne)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Index", "Home");
            }
            return View(ogloszenie);
        }

        // GET: Ogloszenies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = db.Ogloszenies.Find(id);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }

        // POST: Ogloszenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ogloszenie ogloszenie = db.Ogloszenies.Find(id);
            db.Ogloszenies.Remove(ogloszenie);
            db.SaveChanges();
            if (ogloszenie.Prywatne)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Index", "Home");
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
