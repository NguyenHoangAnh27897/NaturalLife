using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;
using PagedList;
using PagedList.Mvc;

namespace NaturalLife.Controllers.WebMaster
{
    public class RoomMasterController : Controller
    {
        natu0679_NaturalLifeEntities db = new natu0679_NaturalLifeEntities();
        // GET: RoomMaster
        public ActionResult List(int? page =1)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var lst = db.NTL_Room.ToList();
            return View(lst.ToPagedList(pageNumber,pageSize));
        }
    }
}