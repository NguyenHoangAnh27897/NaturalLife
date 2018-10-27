using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.WebMaster
{
    public class SettingController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Setting
        public ActionResult Edit()
        {        
                if (Session["Authentication"] != null)
                {
                    var rs = db.NTL_Setting.Where(s => s.ID == 1);
                    return View(rs);
                }
                else
                {
                    return RedirectToAction("Login", "Webmaster");
                }
        }
        [HttpPost]
        public ActionResult Edit(string title1, string title2, string title3, string title4, HttpPostedFileBase image1, HttpPostedFileBase image2, HttpPostedFileBase image3, HttpPostedFileBase image4, string des1, string des2, string des3, string des4)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    string Image01 = "";
                    if (image1 != null)
                    {
                        if (image1.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(image1.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imagenav"), fname);
                            image1.SaveAs(path);
                            Image01 += fname;
                        }

                    }
                    string Image02 = "";
                    if (image2 != null)
                    {
                        if (image2.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(image2.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imagenav"), fname);
                            image2.SaveAs(path);
                            Image02 += fname;
                        }

                    }
                    string Image03 = "";
                    if (image3 != null)
                    {
                        if (image3.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(image3.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imagenav"), fname);
                            image3.SaveAs(path);
                            Image03 += fname;
                        }

                    }
                    string Image04 = "";
                    if (image4 != null)
                    {
                        if (image4.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(image4.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imagenav"), fname);
                            image4.SaveAs(path);
                            Image04 += fname;
                        }

                    }
                    var rs = db.NTL_Setting.Find(1);
                    rs.Title01 = title1;
                    rs.Title02 = title2;
                    rs.Title03 = title3;
                    rs.Title04 = title4;
                    rs.Description01 = des1;
                    rs.Description02 = des2;
                    rs.Description03 = des3;
                    rs.Description04 = des4;
                    if (Image01 != "")
                    {
                        rs.Image01 = Image01;
                    }
                    if (Image02 != "")
                    {
                        rs.Image02 = Image02;
                    }
                    if (Image03 != "")
                    {
                        rs.Image03 = Image03;
                    }
                    if (Image04 != "")
                    {
                        rs.Image04 = Image04;
                    }
                    db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Edit");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("FourOFour","Error");
                }
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        public ActionResult EditAbout()
        {
            if (Session["Authentication"] != null)
            {
                var rs = db.NTL_About.Where(s => s.ID == 1);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpPost]
        public ActionResult EditAbout(string title1, string title2, string title3, string title4, string title5, string title6, string des1, string des2, string des3, string des4, string des5, string des6, string title, string subtitle, string ltitle, string ldes, string rtitle, string rdes, string slogan, string icon1, string icon2, string icon3, string icon4, string icon5, string icon6)
        {
            if (Session["Authentication"] != null)
            {
                try
                {                
                    var rs = db.NTL_About.Find(1);
                    rs.BigTitle = title;
                    rs.SubTitle = subtitle;
                    rs.LeftTitle = ltitle;
                    rs.LeftDescription = ldes;
                    rs.RightTitle = rtitle;
                    rs.RightDescription = rdes;
                    rs.Title1 = title1;
                    rs.Title2 = title2;
                    rs.Title3 = title3;
                    rs.Title4 = title4;
                    rs.Title5 = title5;
                    rs.Title6 = title6;
                    rs.Description1 = des1;
                    rs.Description2 = des2;
                    rs.Description3 = des3;
                    rs.Description4 = des4;
                    rs.Description5 = des5;
                    rs.Desciption6 = des6;
                    rs.Slogan = slogan;
                    rs.Icon1 = icon1;
                    rs.Icon2 = icon2;
                    rs.Icon3 = icon3;
                    rs.Icon4 = icon4;
                    rs.Icon5 = icon5;
                    rs.Icon6 = icon6;
                    db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("EditAbout");
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

        public ActionResult EditPhone()
        {
            if (Session["Authentication"] != null)
            {
                var rs = db.NTL_About.Where(s => s.ID == 1);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpPost]
        public ActionResult EditPhone(string phone1, string phone2, string phone3)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    var rs = db.NTL_About.Find(1);
                    rs.Phone01 = phone1;
                    rs.Phone02 = phone2;
                    rs.Phone03 = phone3;
                    db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("EditPhone");
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