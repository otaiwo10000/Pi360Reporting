
using log4net;
using MPR.Report.Core.Entities;
using MPR.Report.Data.Business;
using Pi360Reporting.Models.AccountModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ClosedXML.Excel;

namespace Pi360Reporting.Controllers
{
    public class ExcelFileDownLoadController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["FintrakDBConnection"].ConnectionString;

        public ActionResult ExcelFileDownLoadOnClient()
        //public void ExcelFileDownLoadOnClient()
        {
            ////string constr = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string sqlquery = "SELECT * FROM Raw_ExpensesFixed";

                using (SqlCommand cmd = new SqlCommand(sqlquery))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            using (XLWorkbook wb = new XLWorkbook())
                            {
                                wb.Worksheets.Add(dt, "Raw_ExpensesFixed");

                                //string AppLocation = "";
                                //AppLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                                //AppLocation = AppLocation.Replace("file:\\", "");
                                //string date = DateTime.Now.ToShortDateString();
                                //date = date.Replace("/", "_");
                                //string filepath = AppLocation + "\\ExcelFiles\\" + "RECEIPTS_COMPARISON_3" + date + ".xlsx";

                                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                wb.Style.Font.Bold = true;
                                //wb.SaveAs(filepath);

                                Response.Clear();
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");

                                using (MemoryStream MyMemoryStream = new MemoryStream())
                                {
                                    wb.SaveAs(MyMemoryStream);
                                    MyMemoryStream.WriteTo(Response.OutputStream);
                                    Response.Flush();
                                    Response.End();
                                }
                            }
                        }
                    }
                }
            }

            return View();
        }
    }
}