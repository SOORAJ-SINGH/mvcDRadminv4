using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC5BoostrapDRAdminV4
{
    public partial class TBL_EmployeeLogin
    {
        [Required]
        public int EmpID { get; set; }
        [Required]
        public string Type { get; set; }
        
    }
}