using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSPOC.BusinessObject
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? EmployeeAge { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string EmployeeEmail { get; set; }
    }
}