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
    public class RoomMasterController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: RoomMaster
        public ActionResult List(int? page = 1)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int pageSize = 7;
                    int pageNumber = (page ?? 1);
                    var lst = db.NTL_Room.ToList();
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
                var lst = db.NTL_RoomType.ToList();
                return View(lst);
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
        public ActionResult Add(string roomname, string Price, string editor, HttpPostedFileBase[] images, HttpPostedFileBase avatar, string roomtype)
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
                            var path = Path.Combine(Server.MapPath("~/Images/website/imageroom"), fname);
                            avatar.SaveAs(path);
                            Avatar += fname;
                        }

                    }
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

                    var room = new NTL_Room();
                    room.ID = getGUID();
                    room.RoomName = roomname;
                    Price = Price.Replace(",", "");
                    var pri = int.Parse(Price);
                    room.Price = pri;
                    if (Avatar != "")
                    {
                        room.Avatar = Avatar;
                    }
                    if (Images != "")
                    {
                        room.Images = Images;
                    }
                    room.Description = editor;
                    int rtype = int.Parse(roomtype);
                    room.RoomTypeID = rtype;
                    db.NTL_Room.Add(room);
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

        public ActionResult Edit(string ID)
        {
            if (Session["Authentication"] != null)
            {
                var rs = db.NTL_Room.Where(s => s.ID.Equals(ID));
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string ID, string roomname, string Price, string editor, HttpPostedFileBase[] images, HttpPostedFileBase avatar, string roomtype)
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
                            var path = Path.Combine(Server.MapPath("~/Images/website/imageroom"), fname);
                            avatar.SaveAs(path);
                            Avatar += fname;
                        }

                    }
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
                    var room = db.NTL_Room.Find(ID);
                    room.RoomName = roomname;
                    Price = Price.Replace(",", "");
                    var pri = int.Parse(Price);
                    room.Price = pri;
                    if (Avatar != "")
                    {
                        room.Avatar = Avatar;
                    }
                    if (Images != "")
                    {
                        room.Images = Images;
                    }
                    room.Description = editor;
                    int rtype = int.Parse(roomtype);
                    room.RoomTypeID = rtype;
                    db.Entry(room).State = System.Data.Entity.EntityState.Modified;
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
                    var rs = db.NTL_Room.Find(ID);
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

        public ActionResult ListType(int? page = 1)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int pageSize = 5;
                    int pageNumber = (page ?? 1);
                    var lst = db.NTL_RoomType.ToList();
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

        public ActionResult AddType()
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
        public ActionResult AddType(string typename, string editor, HttpPostedFileBase[] images, string icon1, string icon2, string icon3)
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
                                    var path = Path.Combine(Server.MapPath("~/Images/website/imageroomtype"), fname);
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
                    var type = new NTL_RoomType();
                    type.RoomType = typename;
                    type.Description = editor;
                    if (Images != "")
                    {
                        type.Images = Images;
                    }
                    type.Icon01 = icon1;
                    type.Icon02 = icon2;
                    type.Icon03 = icon3;
                    db.NTL_RoomType.Add(type);
                    db.SaveChanges();
                    return RedirectToAction("ListType");
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

        public ActionResult EditType(string ID)
        {
            if (Session["Authentication"] != null)
            {
                int id = int.Parse(ID);
                var rs = db.NTL_RoomType.Where(s => s.ID == id);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditType(string typename, string editor, HttpPostedFileBase[] images, string ID, string icon1, string icon2, string icon3)
        {
            if (Session["Authentication"] != null)
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
                                var path = Path.Combine(Server.MapPath("~/Images/website/imageroomtype"), fname);
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
                int id = int.Parse(ID);
                var type = db.NTL_RoomType.Find(id);
                type.RoomType = typename;
                type.Description = editor;
                if (Images != "")
                {
                    type.Images = Images;
                }
                type.Icon01 = icon1;
                type.Icon02 = icon2;
                type.Icon03 = icon3;
                db.Entry(type).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListType");
            }
            else
            {
                return RedirectToAction("Login", "Webmaster");
            }
        }

        [HttpGet]
        public ActionResult DeleteType(string ID)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int id = int.Parse(ID);
                    var rs = db.NTL_RoomType.Find(id);
                    db.Entry(rs).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    return RedirectToAction("ListType");
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