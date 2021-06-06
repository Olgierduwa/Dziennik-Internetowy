using Microsoft.AspNet.Identity;
using ProjektPiotrMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ProjektPiotrMVC.Models.ApplicationDbContext;

namespace ProjektPiotrMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            string UserID = HttpContext.User.Identity.GetUserId();
            var user = db.Users.Find(UserID);
            string RoleID = user.Roles.FirstOrDefault().RoleId;
            ViewBag.UserRole = db.Roles.Find(RoleID).Name;
            var ogloszenies = db.Ogloszenies.Where(m => m.Prywatne == false);
            return View(ogloszenies.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "XDDD.";

            return View();
        }
    }
}