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
        natu0679_NaturalLifeEntities db = new natu0679_NaturalLifeEntities();
        // GET: Homepage
        public ActionResult Edit()
        {
            var rs = db.NTL_Slider.Where(s=>s.ID == 1);
            return View(rs);
        }
    }
}