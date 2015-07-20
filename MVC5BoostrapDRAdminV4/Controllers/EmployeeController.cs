using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5BoostrapDRAdminV4;
using PagedList;
namespace MVC5BoostrapDRAdminV4.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private somya_DR_DBcontext db = new somya_DR_DBcontext();

        // GET: /Employee/
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            //
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var tbl_employeedetails = db.TBL_EmployeeDetails.Include(t => t.TBL_Department).Include(t => t.TBL_Gender).Include(t => t.TBL_OfficeLocation);
            //searching criteria
            if (!String.IsNullOrEmpty(searchString))
            {
                tbl_employeedetails = tbl_employeedetails.Where(s => s.FirstName.Contains(searchString)
                                                                ||
                                                                s.LastName.Contains(searchString));

            }


            //sorting
            ViewBag.EmpIDSortParam = sortOrder == "empID_desc" ? "empID" : "empID_desc";
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.DesignationSortParam = sortOrder == "designation" ? "designation_desc" : "designation";


            switch (sortOrder)
            {
                case "empID_desc":
                    tbl_employeedetails = tbl_employeedetails.OrderByDescending(s => s.EmpID);
                    break;
                case "name":
                    tbl_employeedetails = tbl_employeedetails.OrderBy(s => s.FirstName);
                    break;
                case "name_desc":
                    tbl_employeedetails = tbl_employeedetails.OrderByDescending(s => s.FirstName);
                    break;
                case "designation":
                    tbl_employeedetails = tbl_employeedetails.OrderBy(s => s.Designation);
                    break;
                case "designation_desc":
                    tbl_employeedetails = tbl_employeedetails.OrderByDescending(s => s.Designation);
                    break;

                default:
                    tbl_employeedetails = tbl_employeedetails.OrderBy(s => s.EmpID);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(tbl_employeedetails.ToPagedList(pageNumber, pageSize));
            //return View(tbl_employeedetails.ToList());
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EmployeeDetails tbl_employeedetails = db.TBL_EmployeeDetails.Find(id);
            if (tbl_employeedetails == null)
            {
                return HttpNotFound();
            }

            int tlID = Convert.ToInt32(tbl_employeedetails.TeamLeaderID);
            var TL = db.TBL_EmployeeDetails.Find(tlID);
            if (TL != null)
            {
                ViewBag.TLName = TL.FirstName + " " + TL.LastName;
            }
            else
            {
                ViewBag.TLName = "N.A.";
            }



            int ShiftPartnerID = Convert.ToInt32(tbl_employeedetails.ShiftPartnerID);
            var ShiftPartner = db.TBL_EmployeeDetails.Find(ShiftPartnerID);
            if (ShiftPartner != null)
            {
                ViewBag.ShiftPartnerName = ShiftPartner.FirstName + " " + ShiftPartner.LastName;
            }
            else
            {
                ViewBag.ShiftPartnerName = "N.A.";
            }



            return View(tbl_employeedetails);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {

            ViewBag.DepartmentID = new SelectList(db.TBL_Department, "DepatmentID", "DepartmentName");
            ViewBag.GenderID = new SelectList(db.TBL_Gender, "GenderID", "Gender");
            ViewBag.OfficeLocationID = new SelectList(db.TBL_OfficeLocation, "OfficeLocationID", "OfficeLocation");
            ViewBag.TeamLeaderID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");
            ViewBag.ShiftPartnerID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");


            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpID,FirstName,LastName,GenderID,Designation,DepartmentID,Email,ContactNum,Address,City,DOB,DOA,DOJ,BloodGroup,Mode,OfficeLocationID,TeamLeaderID,ShiftPartnerID")] TBL_EmployeeDetails tbl_employeedetails)
        {
            if (ModelState.IsValid)
            {
                db.TBL_EmployeeDetails.Add(tbl_employeedetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.TBL_Department, "DepatmentID", "DepartmentName", tbl_employeedetails.DepartmentID);
            ViewBag.GenderID = new SelectList(db.TBL_Gender, "GenderID", "Gender", tbl_employeedetails.GenderID);
            ViewBag.OfficeLocationID = new SelectList(db.TBL_OfficeLocation, "OfficeLocationID", "OfficeLocation", tbl_employeedetails.OfficeLocationID);
            ViewBag.TeamLeaderID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");
            ViewBag.ShiftPartnerID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");
            //tbl_employeedetails.TeamLeaderID = "";
            return View(tbl_employeedetails);
        }

        // GET: /Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EmployeeDetails tbl_employeedetails = db.TBL_EmployeeDetails.Find(id);
            if (tbl_employeedetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.TBL_Department, "DepatmentID", "DepartmentName", tbl_employeedetails.DepartmentID);
            ViewBag.GenderID = new SelectList(db.TBL_Gender, "GenderID", "Gender", tbl_employeedetails.GenderID);
            ViewBag.OfficeLocationID = new SelectList(db.TBL_OfficeLocation, "OfficeLocationID", "OfficeLocation", tbl_employeedetails.OfficeLocationID);
            
            //Get the TeamLeader ID and check is it zero or not? 
            int tlID = Convert.ToInt32( tbl_employeedetails.TeamLeaderID);
            if (tlID != 0)
            {
                ViewBag.TeamLeaderID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName", tlID);
            }
            else
            {
                ViewBag.TeamLeaderID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");
            }

            int ShiftPartnerID = Convert.ToInt32(tbl_employeedetails.ShiftPartnerID);
            if (ShiftPartnerID != 0)
            {
                ViewBag.ShiftPartnerID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName",ShiftPartnerID);
            }
            else
            {
                ViewBag.ShiftPartnerID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");
            }
            
            return View(tbl_employeedetails);
        }

        // POST: /Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpID,FirstName,LastName,GenderID,Designation,DepartmentID,Email,ContactNum,Address,City,DOB,DOA,DOJ,BloodGroup,Mode,OfficeLocationID,TeamLeaderID,ShiftPartnerID")] TBL_EmployeeDetails tbl_employeedetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_employeedetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.TBL_Department, "DepatmentID", "DepartmentName", tbl_employeedetails.DepartmentID);
            ViewBag.GenderID = new SelectList(db.TBL_Gender, "GenderID", "Gender", tbl_employeedetails.GenderID);
            ViewBag.OfficeLocationID = new SelectList(db.TBL_OfficeLocation, "OfficeLocationID", "OfficeLocation", tbl_employeedetails.OfficeLocationID);
            ViewBag.TeamLeaderID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");
            ViewBag.ShiftPartnerID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");
            return View(tbl_employeedetails);
        }

        // GET: /Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EmployeeDetails tbl_employeedetails = db.TBL_EmployeeDetails.Find(id);
            if (tbl_employeedetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_employeedetails);
        }

        // POST: /Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_EmployeeDetails tbl_employeedetails = db.TBL_EmployeeDetails.Find(id);
            db.TBL_EmployeeDetails.Remove(tbl_employeedetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
