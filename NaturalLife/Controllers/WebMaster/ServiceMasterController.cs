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
    public class ServiceMasterController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: ServiceMaster
        public ActionResult List(int? page = 1)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int pageSize = 7;
                    int pageNumber = (page ?? 1);
                    var lst = db.NTL_Service.ToList();
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

        //generate new id
        public static string getGUID()
        {
            string rs = "NATURAL";
            Random rd = new Random();
            int random = rd.Next(90000);
            rs += random.ToString() + "_";
            random = rd.Next(90000);
            rs += random.ToString();
            return rs;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(string servicename, string Price, string editor, HttpPostedFileBase[] images, HttpPostedFileBase avatar, string servicetype)
        {
            if (Session["Authentication"] != null)
            {
                if (images != null)
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
                                var path = Path.Combine(Server.MapPath("~/Images/website/imageservice"), fname);
                                avatar.SaveAs(path);
                                Avatar += fname;
                            }

                        }
                        string Images = "";
                        foreach (HttpPostedFileBase file in images)
                        {
                            if (file != null)
                            {
                                if (file.ContentLength > 0)
                                {
                                    var filename = Path.GetFileName(file.FileName);
                                    var fname = filename.Replace(" ", "_");
                                    var path = Path.Combine(Server.MapPath("~/Images/website/imageservice"), fname);
                                    file.SaveAs(path);
                                    Images += fname + ",";
                                }
                            }
                        }
                        if (Images != "" && Images.Contains(","))
                        {
                            Images = Images.Remove(Images.Length - 1);
                        }
                        var sv = new NTL_Service();
                        sv.ID = getGUID();
                        sv.ServiceName = servicename;
                        Price = Price.Replace(",", "");
                        var pri = int.Parse(Price);
                        sv.Price = pri;
                        if (Avatar != "")
                        {
                            sv.Avatar = Avatar;
                        }
                        if (Images != "")
                        {
                            sv.Images = Images;
                        }
                        sv.Description = editor;
                        sv.ServiceType = servicetype;
                        db.NTL_Service.Add(sv);
                        db.SaveChanges();
                        return RedirectToAction("List");
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("FourOFour", "Error");
                    }

                }
                return RedirectToAction("Login", "Webmaster");
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        public ActionResult Edit(string ID)
        {
            if (Session["Authentication"] != null)
            {
                var rs = db.NTL_Service.Where(s => s.ID.Equals(ID));
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edits(string ID,string servicename, string Price, string editor, HttpPostedFileBase[] images, HttpPostedFileBase avatar, string servicetype)
        {
            if (Session["Authentication"] != null)
            {
                if (images != null)
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
                                var path = Path.Combine(Server.MapPath("~/Images/website/imageservice"), fname);
                                avatar.SaveAs(path);
                                Avatar += fname;
                            }

                        }
                        string Images = "";
                        foreach (HttpPostedFileBase file in images)
                        {
                            if (file != null)
                            {
                                if (file.ContentLength > 0)
                                {
                                    var filename = Path.GetFileName(file.FileName);
                                    var fname = filename.Replace(" ", "_");
                                    var path = Path.Combine(Server.MapPath("~/Images/website/imageservice"), fname);
                                    file.SaveAs(path);
                                    Images += fname + ",";
                                }
                            }
                        }
                        if (Images != "" && Images.Contains(","))
                        {
                            Images = Images.Remove(Images.Length - 1);
                        }
                        var sv = db.NTL_Service.Find(ID);
                        sv.ServiceName = servicename;
                        Price = Price.Replace(",", "");
                        var pri = int.Parse(Price);
                        sv.Price = pri;
                        if (Avatar != "")
                        {
                            sv.Avatar = Avatar;
                        }
                        if (Images != "")
                        {
                            sv.Images = Images;
                        }
                        sv.Description = editor;
                        sv.ServiceType = servicetype;
                        db.Entry(sv).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("List");
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("FourOFour", "Error");
                    }

                }
                return RedirectToAction("Login", "Webmaster");
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
                    var rs = db.NTL_Service.Find(ID);
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