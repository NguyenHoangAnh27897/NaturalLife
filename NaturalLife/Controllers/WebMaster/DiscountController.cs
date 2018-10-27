using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using NaturalLife.Models;
using System.IO;

namespace NaturalLife.Controllers.WebMaster
{
    public class DiscountController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: Discount
        public ActionResult List(int? page = 1)
        {
            if (Session["Authentication"] != null)
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                var lst = db.NTL_DiscountProgram.ToList();
                return View(lst.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
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
                return RedirectToAction("Login", "WebMaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(string name, int discountprice, string editor,HttpPostedFileBase avatar)
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
                            var path = Path.Combine(Server.MapPath("~/Images/website/imagediscount"), fname);
                            avatar.SaveAs(path);
                            Avatar += fname;
                        }
                    }
                    var sv = new NTL_DiscountProgram();
                    sv.ProgramName = name;
                    sv.DiscountPrice = discountprice;
                    sv.Description = editor;
                    if(Avatar != null)
                    {
                        sv.Images = Avatar;
                    }
                    db.NTL_DiscountProgram.Add(sv);
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
                int id = int.Parse(ID);
                var rs = db.NTL_DiscountProgram.Where(s => s.ID == id);
                return View(rs);
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(string ID, string name, int discountprice, string editor,HttpPostedFileBase images)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    string Avatar = "";
                    if (images != null)
                    {
                        if (images.ContentLength > 0)
                        {
                            var filename = Path.GetFileName(images.FileName);
                            var fname = filename.Replace(" ", "_");
                            var path = Path.Combine(Server.MapPath("~/Images/website/imagediscount"), fname);
                            images.SaveAs(path);
                            Avatar += fname;
                        }
                    }
                    int id = int.Parse(ID);
                    var sv = db.NTL_DiscountProgram.Find(id);
                    sv.ProgramName = name;
                    sv.DiscountPrice = discountprice;
                    sv.Description = editor;
                    if(Avatar != null)
                    {
                        sv.Images = Avatar;
                    }
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
                    var sv = db.NTL_DiscountProgram.Find(id);
                    db.Entry(sv).State = System.Data.Entity.EntityState.Deleted;
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
                return RedirectToAction("Login", "WebMaster");
            }
        }

        public ActionResult Choose(int id)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    Session["DiscountID"] = id;
                    var ro = db.NTL_Room.ToList();
                    var sv = db.NTL_Service.ToList();
                    Data.Discount di = new Data.Discount();
                    di.room = ro;
                    di.service = sv;
                    List<Data.Discount> lst = new List<Data.Discount>();
                    lst.Add(di);
                    return View(lst);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("FourOFour", "Error");
                }
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        [HttpPost]
        public ActionResult SendService(IEnumerable<string> serviceid, string discountid)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int iddiscount = int.Parse(discountid);
                    foreach (var item in serviceid)
                    {
                        var rs = db.NTL_Service.Find(item);
                        rs.DiscountID = iddiscount;
                        db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Json(new { msg = "Thành công", status = "200" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { msg = "Thất bại", status = "500" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        [HttpPost]
        public ActionResult SendRoom(IEnumerable<string> roomid)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    foreach (var item in roomid)
                    {
                        var rs = db.NTL_Room.Find(item);
                        rs.DiscountID = null;
                        db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Json(new { msg = "Thành công", status = "200" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { msg = "Thất bại", status = "500" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        public ActionResult DeleteChoose(int id)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    var ro = db.NTL_Room.Where(s=>s.DiscountID == id).ToList();
                    var sv = db.NTL_Service.Where(s=>s.DiscountID == id).ToList();
                    Data.Discount di = new Data.Discount();
                    di.room = ro;
                    di.service = sv;
                    List<Data.Discount> lst = new List<Data.Discount>();
                    lst.Add(di);
                    return View(lst);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("FourOFour", "Error");
                }
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        [HttpPost]
        public ActionResult DeleteService(IEnumerable<string> serviceid, string discountid)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    int iddiscount = int.Parse(discountid);
                    foreach (var item in serviceid)
                    {
                        var rs = db.NTL_Service.Find(item);
                        rs.DiscountID = iddiscount;
                        db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Json(new { msg = "Thành công", status = "200" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { msg = "Thất bại", status = "500" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }

        [HttpPost]
        public ActionResult DeleteRoom(IEnumerable<string> roomid)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    foreach (var item in roomid)
                    {
                        var rs = db.NTL_Room.Find(item);
                        rs.DiscountID = null;
                        db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Json(new { msg = "Thành công", status = "200" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { msg = "Thất bại", status = "500" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("Login", "WebMaster");
            }
        }
    }
}