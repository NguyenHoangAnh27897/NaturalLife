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
    public class DiscountController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Discount
        public ActionResult List(int? page = 1)
        {
            if (Session["Authentication"] != null)
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                var lst = db.NTL_DiscountProgram.ToList();
                return View(lst.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        public ActionResult Add()
        {
            if (Session["Authentication"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(string name, int discountprice, string editor)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    var sv = new NTL_DiscountProgram();
                    sv.ProgramName = name;
                    sv.DiscountPrice = discountprice;
                    sv.Description = editor;
                    db.NTL_DiscountProgram.Add(sv);
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

        public ActionResult Edit(string ID)
        {
            if (Session["Authentication"] != null)
            {
                int id = int.Parse(ID);
                var rs = db.NTL_DiscountProgram.Where(s => s.ID == id);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string ID, string name, int discountprice, string editor)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int id = int.Parse(ID);
                    var sv = db.NTL_DiscountProgram.Find(id);
                    sv.ProgramName = name;
                    sv.DiscountPrice = discountprice;
                    sv.Description = editor;
                    db.Entry(sv).State = System.Data.Entity.EntityState.Modified;
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

        [HttpGet]
        public ActionResult Delete(string ID)
        {
            if (Session["Authentication"] != null)
            {
                int id = int.Parse(ID);
                var sv = db.NTL_DiscountProgram.Find(id);
                db.Entry(sv).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }
    }
}