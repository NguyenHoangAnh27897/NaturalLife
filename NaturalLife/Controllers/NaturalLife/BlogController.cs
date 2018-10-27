using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.NaturalLife
{
    public class BlogController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Blog
        public ActionResult Index(int? page = 1)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var lst = db.NTL_Blog.OrderByDescending(s => s.CreatedDate).ToList();
            return View(lst.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Detail(int id)
        {
            var rs = db.NTL_Blog.Where(s => s.ID == id);
            return View(rs);
        }
    }
}