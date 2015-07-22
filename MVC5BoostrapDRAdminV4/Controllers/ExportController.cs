using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5BoostrapDRAdminV4.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Data;
using System.Reflection;
using System.Globalization;


namespace MVC5BoostrapDRAdminV4.Controllers
{
    [Authorize]
    public class ExportController : Controller
    {
        private somya_DR_DBcontext db = new somya_DR_DBcontext();

        //
        // GET: /Export/
        public ActionResult Index(int? empID, string startDate, string endDate, int? isExport)
        {
            try
            {
                 
                var tbl_employeedetails = db.TBL_EmployeeDetails;
                //initialze view bag
                ViewBag.EmpID = empID;
                ViewBag.StartDate = startDate;
                ViewBag.EndDate = endDate;

                //to create the DropDownlist of the Employee Name on the Export page
                ViewBag.EmployeeID = new SelectList(db.TBL_EmployeeDetails, "empID", "FullName");


                //0 means true
                //If Export Button is Clicked.
                #region "If Export Button is Clicked"


                if (isExport == 0)
                {
                    #region Export Process
                    JobsModel jm = new JobsModel();
                    jm.EmpID = Convert.ToInt32(empID);
                    jm.startDate = startDate;
                    jm.endDate = endDate;

                    //ViewBag.EmployeeeName = jm.GetName();

                    //return View(jm.GetJobDetails());

                    GridView gv = new GridView();
                    DataTable dtTemp = new DataTable();
                    dtTemp = ToDataTable(jm.GetJobDetails());
                    //dtTemp.Columns.Remove("JobID");

                    gv.DataSource = dtTemp;
                    gv.DataBind();

                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=DrExport.xls");
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gv.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                    Response.Redirect("/Export", true);
                    return View();
                    #endregion Export Process
                }
                #endregion
                else
                {
                    //If page is requested First Time then empid is null
                    if (empID == null)
                    {
                        ViewBag.SearchCliked = "Unclicked";
                        JobsModel jm = new JobsModel();
                        //jm.GetJobDetails("empty")
                        return View();
                    }
                    //if Search button is clicked
                    else
                    {
                        ViewBag.SearchCliked = "Clicked";

                        JobsModel jm = new JobsModel();
                        jm.EmpID = Convert.ToInt32(empID);
                        
                        jm.startDate = startDate;
                        jm.endDate = endDate;

                        //ViewBag.EmployeeId = jm.EmpID;
                        ViewBag.EmployeeeName = tbl_employeedetails.Find(empID).FirstName;

                        return View("Index", jm.GetJobDetails());

                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        //}

        public void ExportData(int empID, string startDate, string endDate)
        {
            JobsModel jm = new JobsModel();
            jm.EmpID = Convert.ToInt32(empID);
            jm.startDate = startDate;
            jm.endDate = endDate;

            //ViewBag.EmployeeeName = jm.GetName();

            //return View(jm.GetJobDetails());

            GridView gv = new GridView();
            //gv.DataSource = db.Studentrecord.ToList();
            gv.DataSource = jm.GetJobDetails();

            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Marklist.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            //return RedirectToAction("StudentDetails");
        }



        //Convert to the Ilist to the DataTable
        public DataTable ToDataTable<T>(List<T> items)
        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {

                //Setting column names as Property names

                dataTable.Columns.Add(prop.Name);

            }

            foreach (T item in items)
            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {

                    //inserting property values to datatable rows

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

            //put a breakpoint here and check datatable

            return dataTable;

        }


    }
}