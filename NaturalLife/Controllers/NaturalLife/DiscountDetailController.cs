using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.NaturalLife
{
    public class DiscountDetailController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: DiscountDetail
        public ActionResult Index(int id)
        {
            var rs = db.NTL_DiscountProgram.Where(s => s.ID == id);
            return View(rs);
        }
    }
}