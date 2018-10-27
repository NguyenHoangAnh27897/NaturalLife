using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;
using PagedList;
using PagedList.Mvc;


namespace NaturalLife.Controllers.WebMaster
{
    public class BlogMasterController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: BlogMaster
        public ActionResult List(int? page = 1)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int pageSize = 7;
                    int pageNumber = (page ?? 1);
                    var lst = db.NTL_Blog.ToList();
                    return View(lst.ToPagedList(pageNumber, pageSize));
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

        public ActionResult Add()
        {
            if (Session["Authentication"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(string title, string createdby, string editor, HttpPostedFileBase avatar)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    string Avatar = "";
                    if (avatar != null)
                    {
                        if (avatar.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(avatar.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imageblog"), fname);
                            avatar.SaveAs(path);
                            Avatar += fname;
                        }

                    }               
                    var sv = new NTL_Blog();
                    sv.Title = title;
                    sv.CreatedBy = createdby;
                    if (Avatar != "")
                    {
                        sv.Avatar = Avatar;
                    }
                    sv.CreatedDate = DateTime.Now;
                    sv.ContentBlog = editor;
                    db.NTL_Blog.Add(sv);
                    db.SaveChanges();
                    return RedirectToAction("List");
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

        public ActionResult Edit(string id)
        {
            if (Session["Authentication"] != null)
            {
                int ID = int.Parse(id);
                var rs = db.NTL_Blog.Where(s => s.ID == ID);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string ID,string title, string createdby, string editor, HttpPostedFileBase avatar)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    string Avatar = "";
                    if (avatar != null)
                    {
                        if (avatar.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(avatar.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imageblog"), fname);
                            avatar.SaveAs(path);
                            Avatar += fname;
                        }

                    }
                    int id = int.Parse(ID);
                    var sv = db.NTL_Blog.Find(id);
                    sv.Title = title;
                    sv.CreatedBy = createdby;
                    if (Avatar != "")
                    {
                        sv.Avatar = Avatar;
                    }
                    sv.CreatedDate = DateTime.Now;
                    sv.ContentBlog = editor;
                    db.Entry(sv).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List");
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

        [HttpGet]
        public ActionResult Delete(string ID)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int id = int.Parse(ID);
                    var rs = db.NTL_Blog.Find(id);
                    db.Entry(rs).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("List");
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