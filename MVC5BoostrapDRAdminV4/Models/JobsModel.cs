using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5BoostrapDRAdminV4.Models
{
    public class JobsModel
    {

        public int EmpID { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }


        //somyatrans_drbetaEntitiesJob jobdb = new somyatrans_drbetaEntitiesJob();
        somya_DR_DBcontext empdb = new somya_DR_DBcontext();

        //This is called for the Get Action Methodsgets the List of the Job Details of Employee as per Dates selected 
        public List<USP_EXPORT_JOB_Result> GetJobDetails( string empty)
        {
            //DateTime startDate = new DateTime();
            //DateTime endDate = new DateTime();
            //startDate = DateTime.Now.AddDays(-5);

            return empdb.USP_EXPORT_JOB("GETJOBData", 0, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-1)).ToList();

            
        }

        public List<USP_EXPORT_JOB_Result> GetJobDetails()
        {
            //DateTime startDate = new DateTime();
            //DateTime endDate = new DateTime();
            //startDate = DateTime.Now.AddDays(-5);

            //return jobdb.USP_EXPORT_JOB("GETJOBData", 117, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-1)).ToList();
            return empdb.USP_EXPORT_JOB("GETJOBData", EmpID, startDate, endDate).ToList();

        }
          

        //gets the name of the Employee based on the EmpID
        //public List<USP_GETNAME_Result> GetName() 
        //{
        //   // string name = Convert.ToString( );
        //    return empdb.USP_GETNAME("GETNAME", EmpID, startDate, endDate).ToList();

        //}


        //gets the jobs details for range of date of every Employee  
        public List<USP_admin_TotalDRFilled_Result> GetDRFilledDetails(DateTime startDate,DateTime endDate)
        {
            return empdb.USP_admin_TotalDRFilled(startDate, endDate).ToList();
        }

        //get the TL and Emp  Relation from the database with there designation
        public List<USP_admin_TLrelation_Result> GetTLrelation()
        {
            return empdb.USP_admin_TLrelation().ToList();
        }
    }
}