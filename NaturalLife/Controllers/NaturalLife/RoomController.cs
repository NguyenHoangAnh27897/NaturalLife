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
    public class RoomController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Room
        public ActionResult Index(string ID, int? page = 1)
        {
            try
            {
                int pageSize = 9;
                int pageNumber = (page ?? 1);
                int id = int.Parse(ID);
                var lst = db.NTL_Room.Where(s => s.RoomTypeID == id).ToList();
                return View(lst.ToPagedList(pageNumber, pageSize));
            }
            catch(Exception ex)
            {
                return RedirectToAction("FourOFour", "Error");
            }
           
        }

        public ActionResult Detail(string id)
        {
            try
            {
                var rs = db.NTL_Room.Where(s => s.ID.Equals(id));
                var sch = db.NTL_Schedule.Where(s => s.ServiceID == id);
                Data.RoomDetail room = new Data.RoomDetail();
                room.Room = rs;
                room.Schedule = sch;
                List<Data.RoomDetail> lst = new List<Data.RoomDetail>();
                lst.Add(room);
                return View(lst);
            }
            catch (Exception ex)
            {
                return RedirectToAction("FourOFour", "Error");
            }
        }
    }
}