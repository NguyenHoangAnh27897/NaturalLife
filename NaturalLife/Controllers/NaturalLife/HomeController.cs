using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers
{
    public class HomeController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        public ActionResult Index()
        {
            try
            {
                var rs = db.NTL_Slider.Where(s => s.ID == 1);
                var rt = db.NTL_RoomType.ToList();
                Data.HomePage hp = new Data.HomePage();
                hp.RoomType = rt;
                hp.Slider = rs;
                List<Data.HomePage> lst = new List<Data.HomePage>();
                lst.Add(hp);
                return View(lst);
            }
            catch(Exception ex)
            {
                return RedirectToAction("FourOFour", "Error");
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Booking(string room,string fullname, string phone, string adult, string child, string vehicle, string startdate, string enddate, string[] activity, string comment= "",string option1 = "", string option2 = "", string option3 ="", string option4 = "", string option5 = "", string option6 = "", string option7 = "", string option8 = "", string option9 = "")
        {
            try
            {
                string service = "";
                if (option1 != "")
                {
                    service += option1 + ", ";
                }
                if (option2 != "")
                {
                    service += option2 + ", ";
                }
                if (option3 != "")
                {
                    service += option3 + ", ";
                }
                if (option4 != "")
                {
                    service += option4 + ", ";
                }
                if (option5 != "")
                {
                    service += option5 + ", ";
                }
                if (option6 != "")
                {
                    service += option6 + ", ";
                }
                if (option7 != "")
                {
                    service += option7 + ", ";
                }
                if (option8 != "")
                {
                    service += option8 + ", ";
                }
                if (option9 != "")
                {
                    service += option9 + ", ";
                }
                if (service != "")
                {
                    service = service.Remove(service.Length - 2);
                }
                DateTime sDate = DateTime.ParseExact(startdate, "yyyy-MM-dd",
                                      System.Globalization.CultureInfo.InvariantCulture);
                DateTime eDate = DateTime.ParseExact(enddate, "yyyy-MM-dd",
                                      System.Globalization.CultureInfo.InvariantCulture);
                string active = "";
                for (int i = 0; i < activity.Length; i++)
                {
                    active += activity[i] + ", ";
                }
                if(active != "")
                {
                    active = active.Remove(active.Length - 2);
                }
                var book = new NTL_ExtraBooking();
                book.RoomType = room;
                book.Fullname = fullname;
                book.Phone = phone;
                book.NumberOfAdult = adult;
                book.NumberOfChild = child;
                book.VehicleType = vehicle;
                book.StartDate = sDate;
                book.EndDate = eDate;
                book.ActivityID = active;
                book.ServiceID = service;
                db.NTL_ExtraBooking.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                return RedirectToAction("FourOFour", "Error");
            }
           
        }
    }
}