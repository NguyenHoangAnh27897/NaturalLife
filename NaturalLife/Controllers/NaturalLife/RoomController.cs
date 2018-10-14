using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.NaturalLife
{
    public class RoomController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Room
        public ActionResult Index()
        {
            //int id = int.Parse(ID);
            //var lst = db.NTL_Room.Where(s => s.RoomTypeID == id).ToList();
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}