using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.WebMaster
{
    public class HomepageController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Homepage
        public ActionResult Edit()
        {
            if (Session["Authentication"] != null)
            {
                var rs = db.NTL_Slider.Where(s => s.ID == 1);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
           
        }

        [HttpPost]
        public ActionResult Edit(string title, string title2, string title3, string subtitle, string subtitle2, string subtitle3, string slogan, string slogan2, string slogan3, string icon1, string icon2, string icon3, HttpPostedFileBase image1, HttpPostedFileBase image2, HttpPostedFileBase image3, string editor1, string editor2, string editor3)
        {
            if (Session["Authentication"] != null)
            {
                string Image1 = "";
                if (image1 != null)
                {
                    if (image1.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(image1.FileName);
                        var fname = filename.Replace(" ", "_");
                        var path = Path.Combine(Server.MapPath("~/Images/website/imagehome"), fname);
                        image1.SaveAs(path);
                        Image1 += fname;
                    }

                }
                string Image2 = "";
                if (image2 != null)
                {
                    if (image2.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(image2.FileName);
                        var fname = filename.Replace(" ", "_");
                        var path = Path.Combine(Server.MapPath("~/Images/website/imagehome"), fname);
                        image2.SaveAs(path);
                        Image2 += fname;
                    }

                }
                string Image3 = "";
                if (image3 != null)
                {
                    if (image3.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(image3.FileName);
                        var fname = filename.Replace(" ", "_");
                        var path = Path.Combine(Server.MapPath("~/Images/website/imagehome"), fname);
                        image3.SaveAs(path);
                        Image3 += fname;
                    }

                }
                var home = db.NTL_Slider.Find(1);
                home.Title = title;
                home.Title2 = title2;
                home.Title3 = title3;
                home.SubTitle = subtitle;
                home.SubTitle2 = subtitle2;
                home.SubTitle3 = subtitle3;
                home.Slogan = slogan;
                home.Slogan2 = slogan2;
                home.Slogan3 = slogan3;
                home.Icon = icon1;
                home.Icon2 = icon2;
                home.Icon3 = icon3;
                home.Description1 = editor1;
                home.Description2 = editor2;
                home.Description3 = editor3;
                if (Image1 != "")
                {
                    home.Image1 = Image1;
                }
                if (Image2 != "")
                {
                    home.Image2 = Image2;
                }
                if (Image3 != "")
                {
                    home.Image3 = Image3;
                }
                db.Entry(home).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        public ActionResult EditMain()
        {
            if (Session["Authentication"] != null)
            {
                var rs = db.NTL_Main.Where(s => s.ID == 1);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditMain(string main01, string main02, HttpPostedFileBase image1, HttpPostedFileBase image2,string title01, string title02)
        {
            if (Session["Authentication"] != null)
            {
                string Image1 = "";
                if (image1 != null)
                {
                    if (image1.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(image1.FileName);
                        var fname = filename.Replace(" ", "_");
                        var path = Path.Combine(Server.MapPath("~/Images/website/imagehome"), fname);
                        image1.SaveAs(path);
                        Image1 += fname;
                    }

                }
                string Image2 = "";
                if (image2 != null)
                {
                    if (image2.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(image2.FileName);
                        var fname = filename.Replace(" ", "_");
                        var path = Path.Combine(Server.MapPath("~/Images/website/imagehome"), fname);
                        image2.SaveAs(path);
                        Image2 += fname;
                    }

                }
                var home = db.NTL_Main.Find(1);
                main01 = main01.Substring(0, 2) + " class='intro'" + main01.Substring(2);
                main02 = main02.Substring(0, 2) + " class='intro'" + main02.Substring(2);
                home.Content01 = main01;
                home.Content02 = main02;
                home.Title01 = title01;
                home.Title02 = title02;
                if (Image1 != "")
                {
                    home.Image01 = Image1;
                }
                if (Image2 != "")
                {
                    home.Image02 = Image2;
                }
                db.Entry(home).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditMain");
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }
        public ActionResult EditVideo()
        {
            if (Session["Authentication"] != null)
            {
                var rs = db.NTL_Video.Where(s => s.ID == 1);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpPost]
        public ActionResult EditVideo(string title1, string title2, string title3, string Subtitle1, string Subtitle2, string Subtitle3, string des1, string des2, string des3, HttpPostedFileBase video)
        {
            if (Session["Authentication"] != null)
            {
                string Video = "";
                if (video != null)
                {
                    if (video.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(video.FileName);
                        var fname = filename.Replace(" ", "_");
                        var path = Path.Combine(Server.MapPath("~/Video"), fname);
                        video.SaveAs(path);
                        Video += fname;
                    }

                }
                var home = db.NTL_Video.Find(1);
                home.Title01 = title1;
                home.Title02 = title2;
                home.Title03 = title3;
                home.SubTitle01 = Subtitle1;
                home.SubTitle02 = Subtitle2;
                home.SubTitle03 = Subtitle3;
                home.Description01 = des1;
                home.Description02 = des2;
                home.Description03 = des3;
                if(Video != null)
                {
                    home.Video = Video;
                }
                db.Entry(home).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditVideo");
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

       
    }

    
}