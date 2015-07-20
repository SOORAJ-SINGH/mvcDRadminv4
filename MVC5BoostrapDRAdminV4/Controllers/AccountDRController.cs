using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5BoostrapDRAdminV4;

namespace MVC5BoostrapDRAdminV4.Controllers
{
    [Authorize]
    public class AccountDRController : Controller
    {
        private somya_DR_DBcontext db = new somya_DR_DBcontext();

        // GET: /AccountDR/
        public ActionResult Index(string searchString)
        {
            var tbl_employeelogin = db.TBL_EmployeeLogin.Include(t => t.TBL_EmployeeDetails);
            //searching based on the Name
            if(!string.IsNullOrEmpty(searchString))
            {
                tbl_employeelogin = tbl_employeelogin.Where(s => s.TBL_EmployeeDetails.FirstName.Contains(searchString)
                                                            || s.TBL_EmployeeDetails.LastName.Contains(searchString));
            }
            return View(tbl_employeelogin.ToList());
        }

        // GET: /AccountDR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EmployeeLogin tbl_employeelogin = db.TBL_EmployeeLogin.Find(id);
            if (tbl_employeelogin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_employeelogin);
        }

        // GET: /AccountDR/Create
        public ActionResult Create()
        {
            ViewBag.EmpID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName");
            return View();
        }

        // POST: /AccountDR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="LoginId,EmpID,Password,Type,LastLoginOn,CreatedOn")] TBL_EmployeeLogin tbl_employeelogin)
        {
            if (ModelState.IsValid)
            {
                db.TBL_EmployeeLogin.Add(tbl_employeelogin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName", tbl_employeelogin.EmpID);
            return View(tbl_employeelogin);
        }

        // GET: /AccountDR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EmployeeLogin tbl_employeelogin = db.TBL_EmployeeLogin.Find(id);
            if (tbl_employeelogin == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName", tbl_employeelogin.EmpID);
            return View(tbl_employeelogin);
        }

        // POST: /AccountDR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="LoginId,EmpID,Password,Type,LastLoginOn,CreatedOn")] TBL_EmployeeLogin tbl_employeelogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_employeelogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpID = new SelectList(db.TBL_EmployeeDetails, "EmpID", "FirstName", tbl_employeelogin.EmpID);
            return View(tbl_employeelogin);
        }

        // GET: /AccountDR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_EmployeeLogin tbl_employeelogin = db.TBL_EmployeeLogin.Find(id);
            if (tbl_employeelogin == null)
            {
                return HttpNotFound();
            }
            return View(tbl_employeelogin);
        }

        // POST: /AccountDR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_EmployeeLogin tbl_employeelogin = db.TBL_EmployeeLogin.Find(id);
            db.TBL_EmployeeLogin.Remove(tbl_employeelogin);
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
