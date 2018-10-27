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
    public class ContactMasterController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Contact
        public ActionResult List(int? page = 1)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int pageSize = 7;
                    int pageNumber = (page ?? 1);
                    var lst = db.NTL_Contact.ToList();
                    return View(lst.ToPagedList(pageNumber, pageSize));
                }
                catch (Exception ex)
                {
                    return RedirectToAction("FourOFour", "Error");
                }

            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    var rs = db.NTL_Contact.Find(id);
                    db.Entry(rs).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("FourOFour", "Error");
                }

            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }
    }
}