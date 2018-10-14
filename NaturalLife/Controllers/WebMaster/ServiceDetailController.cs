using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.WebMaster
{
    public class ServiceDetailController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: ServiceDetail
        public ActionResult Detail()
        {
            if (Session["Authentication"] != null)
            {
                try
                {
              
                    return View();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("FourOFour", "Error");
                }

            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }
    }
}