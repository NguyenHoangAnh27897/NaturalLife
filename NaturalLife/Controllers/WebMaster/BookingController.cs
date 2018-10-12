using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.WebMaster
{
    public class BookingController : Controller
    {
        natu0679_NaturalLifeEntities db = new natu0679_NaturalLifeEntities();
        // GET: Booking
        public ActionResult List(int? page =1)
        {
            if (Session["Authentication"] != null)
            {
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                var lst = db.NTL_ExtraBooking.ToList();
                return View(lst.ToPagedList(pageNumber,pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        public ActionResult Detail(string id)
        {
            if (Session["Authentication"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        public ActionResult Delete(string id)
        {
            if (Session["Authentication"] != null)
            {
                int ID = int.Parse(id);
                var rs = db.NTL_ExtraBooking.Find(ID);
                db.Entry(rs).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }
    }
}