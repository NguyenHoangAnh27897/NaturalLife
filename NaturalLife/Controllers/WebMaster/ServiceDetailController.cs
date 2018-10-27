using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using NaturalLife.Models;

namespace NaturalLife.Controllers.WebMaster
{
    public class ServiceDetailController : Controller
    {
        NaturalLifeEntities db = new NaturalLifeEntities();
        // GET: ServiceDetail
        public ActionResult Detail(string id)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    var rsid = db.NTL_Service.Where(s => s.ID.Equals(id));
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
                            var path = Path.Combine(Server.MapPath("~/Images/website/imageservice"), fname);
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
                            var path = Path.Combine(Server.MapPath("~/Images/website/imageservice"), fname);
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
                    var rs = db.NTL_Service.Find(ID);
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
                    return RedirectToAction("List", "ServiceMaster");
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
                    }
                    var rs = db.NTL_Service.Find(ID);
                    if (Images != null)
                    {
                        rs.ImageCollection = Images;
                    }
                    db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List", "ServiceMaster");
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
        public ActionResult AddPDF(string ID,string pdffile)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    string pdf = "<iframe src='"+pdffile+ "' width='940' height='780' style='margin-left: 125px;'></iframe>";
                    var rs = db.NTL_Service.Find(ID);
                    rs.PDFLink = pdf;
                    db.Entry(rs).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List", "ServiceMaster");
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
        public ActionResult UploadExcel(string ID, HttpPostedFileBase FileExcel)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    if (Request.Files["FileExcel"].ContentLength > 0)
                    {
                        string fileExtension = System.IO.Path.GetExtension(Request.Files["FileExcel"].FileName);

                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            string fileLocation = Server.MapPath("~/document/excel/") + Request.Files["FileExcel"].FileName;
                            if (System.IO.File.Exists(fileLocation))
                            {
                                System.IO.File.Delete(fileLocation);
                            }
                            Request.Files["FileExcel"].SaveAs(fileLocation);
                            string excelConnectionString = string.Empty;
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //connection String for xls file format.
                            if (fileExtension == ".xls")
                            {
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            }
                            //connection String for xlsx file format.
                            else if (fileExtension == ".xlsx")
                            {
                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            }
                            //Create Connection to Excel work book and add oledb namespace
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            DataTable dt = new DataTable();

                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }

                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                        }
                        NTL_Adventure rs;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            rs = new NTL_Adventure();
                            rs.Time = ds.Tables[0].Rows[i][1].ToString();
                            rs.Activity = ds.Tables[0].Rows[i][2].ToString();
                            rs.DetailActivity = ds.Tables[0].Rows[i][3].ToString();
                            rs.ServiceID = ID;
                            db.NTL_Adventure.Add(rs);
                            db.SaveChanges();
                        }       
                    }
                    return RedirectToAction("List", "ServiceMaster");
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
        public ActionResult UploadExcelPrice(string ID, HttpPostedFileBase FileExcel)
        {
            if (Session["Authentication"] != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    if (Request.Files["FileExcel"].ContentLength > 0)
                    {
                        string fileExtension = System.IO.Path.GetExtension(Request.Files["FileExcel"].FileName);

                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            string fileLocation = Server.MapPath("~/document/excel/") + Request.Files["FileExcel"].FileName;
                            if (System.IO.File.Exists(fileLocation))
                            {
                                System.IO.File.Delete(fileLocation);
                            }
                            Request.Files["FileExcel"].SaveAs(fileLocation);
                            string excelConnectionString = string.Empty;
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //connection String for xls file format.
                            if (fileExtension == ".xls")
                            {
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            }
                            //connection String for xlsx file format.
                            else if (fileExtension == ".xlsx")
                            {
                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            }
                            //Create Connection to Excel work book and add oledb namespace
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();
                            DataTable dt = new DataTable();

                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }

                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                            }
                        }
                        NTL_Schedule rs;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            rs = new NTL_Schedule();
                            rs.Schedule = ds.Tables[0].Rows[i][0].ToString();
                            rs.TourType = ds.Tables[0].Rows[i][1].ToString();
                            string pday = ds.Tables[0].Rows[i][2].ToString();
                            int priceday = int.Parse(pday);
                            rs.PriceDay = priceday;
                            string pweek = ds.Tables[0].Rows[i][3].ToString();
                            int priceweek = int.Parse(pweek);
                            rs.PriceWeekend = priceweek;
                            string phol = ds.Tables[0].Rows[i][4].ToString();
                            int pricehol = int.Parse(phol);
                            rs.PriceHoliday = pricehol;
                            string pallday = ds.Tables[0].Rows[i][7].ToString();
                            int priceallday = int.Parse(pallday);
                            rs.AllInPirceDay = priceallday;
                            string pallweek = ds.Tables[0].Rows[i][8].ToString();
                            int priceallweek = int.Parse(pallweek);
                            rs.AllInPirceWeekend = priceallweek;
                            string pallhol = ds.Tables[0].Rows[i][9].ToString();
                            int priceallhol = int.Parse(pallhol);
                            rs.AllInPirceHoliday = priceallhol;
                            rs.Service = ds.Tables[0].Rows[i][5].ToString();
                            rs.AllInService = ds.Tables[0].Rows[i][6].ToString();
                            rs.Status = ds.Tables[0].Rows[i][10].ToString();
                            rs.ServiceID = ID;
                            db.NTL_Schedule.Add(rs);
                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("List", "RoomDetail");
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