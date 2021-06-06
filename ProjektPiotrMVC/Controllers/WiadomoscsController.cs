using Microsoft.AspNet.Identity;
using ProjektPiotrMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektPiotrMVC.Controllers
{
    public class WiadomoscsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Wiadomoscs
        public ActionResult IndexNauczyciele()
        {
            return RedirectToAction("Index", new { rola = "f073efe4-08eb-4b30-861a-dac97b61c870" });
        }
        public ActionResult IndexRodzice()
        {
            return RedirectToAction("Index", new { rola = "99f4da46-2d68-463e-a75a-c8ad37619092" });
        }
        public ActionResult Index(string rola)
        {
            List<ApplicationUser> users = db.Users.Where(m => m.Roles.FirstOrDefault().RoleId == rola).ToList();
            return View(users);
        }

        public ActionResult Manage(string OdbiorcaID)
        {
            string NadawcaID = HttpContext.User.Identity.GetUserId();
            List<Wiadomosc> wiadomosci = db.Wiadomoscs.Where(m=>m.NadawcaID == NadawcaID && m.OdbiorcaID == OdbiorcaID).ToList();
            ViewBag.NadawcaID = NadawcaID;
            ViewBag.OdbiorcaID = OdbiorcaID;
            ViewBag.Odbiorca = db.Users.Find(OdbiorcaID).Email;
            return View(wiadomosci);
        }

        // GET: Wiadomoscs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Wiadomoscs/Create
        public ActionResult Create(string NadawcaID, string OdbiorcaID, string wiadomosc)
        {

            return View();
        }

        // POST: Wiadomoscs/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wiadomoscs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Wiadomoscs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Wiadomoscs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Wiadomoscs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
