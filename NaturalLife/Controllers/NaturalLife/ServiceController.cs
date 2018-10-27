using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;
using PagedList;
using PagedList.Mvc;

namespace NaturalLife.Controllers.NaturalLife
{
    public class ServiceController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Service
        public ActionResult Index(int? page = 1)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var lst = db.NTL_Service.ToList();
            return View(lst.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Detail(string id)
        {
            var rs = db.NTL_Service.Where(s=>s.ID.Equals(id));
            var ad = db.NTL_Adventure.Where(s => s.ServiceID.Equals(id)).ToList();
            var sch = db.NTL_Schedule.Where(s => s.ServiceID.Equals(id)).ToList();
            Data.ServiceDetail sd = new Data.ServiceDetail();
            sd.Adventure = ad;
            sd.Service = rs;
            sd.Schedule = sch;
            List<Data.ServiceDetail> lst = new List<Data.ServiceDetail>();
            lst.Add(sd);
            return View(lst);
        }
    }
}