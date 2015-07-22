using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace MVC5BoostrapDRAdminV4
{
    public partial class TBL_EmployeeDetails
    {
        [Required]
        [DisplayName("Employee ID")]
        public int EmpID { get; set; }
        [Required]
        [DisplayName("Gender")]
        public int GenderID { get; set; }
        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOB { get; set; }

        [DisplayName("Date of Anniversary")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOA { get; set; }
        [Required]
        [DisplayName("Date of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOJ { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get { return FirstName +" "+ LastName; }  }

        [Required]
        public string Designation { get; set; }
        [DisplayName("Department")]
        public Nullable<int> DepartmentID { get; set; }
        [Required]
        public string Email { get; set; }
        [DisplayName("Contact Number")]
        public string ContactNum { get; set; }
        public string Address { get; set; }
        public string City { get; set; }


        [DisplayName("Blood Group")]
        public string BloodGroup { get; set; }
        [Required]
        public string Mode { get; set; }
        [DisplayName("Office Location")]
        public Nullable<int> OfficeLocationID { get; set; }
        [DisplayName("Team Leader ID")]
        public Nullable<int> TeamLeaderID { get; set; }
        [DisplayName("Shift Partner ID")]
        public Nullable<int> ShiftPartnerID { get; set; }
        
        [DisplayName("Department")]
        public virtual TBL_Department TBL_Department { get; set; }

        
        public virtual TBL_Gender TBL_Gender { get; set; }
        [DisplayName("OfficeLocation")]
        public virtual TBL_OfficeLocation TBL_OfficeLocation { get; set; }
        
        [DisplayName("EmployeeLogin")]
        public virtual ICollection<TBL_EmployeeLogin> TBL_EmployeeLogin { get; set; }

        //public virtual TBL_EmployeeDetails TBL_EmployeeDetails { get; set; }

    }
}