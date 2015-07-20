using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5BoostrapDRAdminV4.Models;

namespace MVC5BoostrapDRAdminV4.Controllers
{
    public class ChartsController : Controller
    {
        JobsModel job = new JobsModel();

        // GET: Charts
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DrFilledBarChart(string startDate,string endDate)
        {

            

            DateTime sd = Convert.ToDateTime(startDate);

            var jobData = job.GetDRFilledDetails(Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            return Json(jobData, JsonRequestBehavior.AllowGet);
        }



        public ActionResult TLChart()
        {
            return View();
        }
        public JsonResult TLOrgChart()
        {
            var charData = job.GetTLrelation();  //calling the methods

            return Json(charData, JsonRequestBehavior.AllowGet); //return list from here

        }

    }
}