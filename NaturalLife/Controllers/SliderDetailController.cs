using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers
{
    public class SliderDetailController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: SliderDetail
        public ActionResult Index()
        {
            var rs = db.NTL_Slider.Where(s => s.ID == 1);
            return View(rs);
        }

        public ActionResult Index2()
        {
            var rs = db.NTL_Slider.Where(s => s.ID == 1);
            return View(rs);
        }

        public ActionResult Index3()
        {
            var rs = db.NTL_Slider.Where(s => s.ID == 1);
            return View(rs);
        }
    }
}