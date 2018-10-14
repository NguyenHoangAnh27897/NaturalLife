using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.WebMaster
{
    public class HomepageController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Homepage
        public ActionResult Edit()
        {
            if (Session["Authentication"] != null)
            {
                var rs = db.NTL_Slider.Where(s => s.ID == 1);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
           
        }

        [HttpPost]
        public ActionResult Edit(string title, string title2, string subtitle, string subtitle2, string slogan, string slogan2, string icon, string icon2, HttpPostedFileBase[] images)
        {
            if (Session["Authentication"] != null)
            {
                var home = db.NTL_Slider.Find(1);
                home.Title = title;
                home.Title2 = title2;
                home.SubTitle = subtitle;
                home.SubTitle2 = subtitle2;
                home.Slogan = slogan;
                home.Slogan2 = slogan2;
                home.Icon = icon;
                home.Icon2 = icon2;
                db.Entry(home).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }
    }
}