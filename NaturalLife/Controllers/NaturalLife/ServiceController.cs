using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.NaturalLife
{
    public class ServiceController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(string id)
        {
            var rs = db.NTL_Service.Where(s=>s.ID.Equals(id));
            return View(rs);
        }
    }
}