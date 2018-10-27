using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaturalLife.Models;

namespace NaturalLife.Controllers.WebMaster
{
    public class RoomDetailController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: RoomDetail
        public ActionResult Index(string id)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    var rsid = db.NTL_Room.Where(s => s.ID.Equals(id));
                    return View(rsid);
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddInfo(string ID, string advan, string disvan, HttpPostedFileBase avatarin, HttpPostedFileBase avatarout)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    string AvatarIn = "";
                    if (avatarin != null)
                    {
                        if (avatarin.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(avatarin.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imageroom"), fname);
                            avatarin.SaveAs(path);
                            AvatarIn += fname;
                        }

                    }
                    string AvatarOut = "";
                    if (avatarout != null)
                    {
                        if (avatarout.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(avatarout.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imageroom"), fname);
                            avatarout.SaveAs(path);
                            AvatarOut += fname;
                        }

                    }
                    if (advan.Contains("ul"))
                    {
                        advan = advan.Substring(0, 3) + " class='content-list tick-list'" + advan.Substring(2);
                    }
                    if (disvan.Contains("ul"))
                    {
                        disvan = disvan.Substring(0, 3) + " class='content-list cross-list'" + disvan.Substring(2);
                    }
                    var rs = db.NTL_Room.Find(ID);
                    rs.Advantage = advan;
                    rs.Disadvantage = disvan;
                    if (AvatarIn != "")
                    {
                        rs.AvatarIn = AvatarIn;
                    }
                    if (AvatarOut != "")
                    {
                        rs.AvatarOut = AvatarOut;
                    }
                    db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List", "RoomMaster");
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

        [HttpPost]
        public ActionResult AddCollection(string ID, HttpPostedFileBase[] images)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    string Images = "";
                    if (images != null)
                    {
                        foreach (HttpPostedFileBase file in images)
                        {
                            if (file != null)
                            {
                                if (file.ContentLength > 0)
                                {
                                    var filename = Path.GetFileName(file.FileName);
                                    var fname = filename.Replace(" ", "_");
                                    var path = Path.Combine(Server.MapPath("~/Images/website/imageroom"), fname);
                                    file.SaveAs(path);
                                    Images += fname + ",";
                                }
                            }
                        }
                        if (Images != "" && Images.Contains(","))
                        {
                            Images = Images.Remove(Images.Length - 1);
                        }
                    }
                    var rs = db.NTL_Room.Find(ID);
                    if (Images != null)
                    {
                        rs.ImageCollection = Images;
                    }
                    db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List", "RoomMaster");
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