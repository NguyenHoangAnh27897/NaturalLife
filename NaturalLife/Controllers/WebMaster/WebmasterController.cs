using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaturalLife.Controllers.WebMaster
{
    public class WebmasterController : Controller
    {
        // GET: Webmaster
        public ActionResult Index()
        {
            if(Session["Authentication"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Webmaster");
            }
           
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            if (Username.Equals("admin"))
            {
                if (Password.Equals("1234"))
                {
                    Session["Authentication"] = "true";
                    return RedirectToAction("Index", "Webmaster");
                }
                else
                {
                   return RedirectToAction("Login", "Webmaster");
                }
            }
            else
            {
              return  RedirectToAction("Login", "Webmaster");
            }
        }
    }
}