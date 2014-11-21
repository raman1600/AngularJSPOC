using AngularJSPOC.BusinessObject;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSPOC.EntityFramework
{
    public class EmployeeDao
    {
        public int SaveEmployee(EmployeeModel objEmployee)
        {
            using (var context = new AngularJSPOCEmployeeEntities())
            {
                var empEntity = Mapper.Map<EmployeeModel, Employee>(objEmployee);
                context.Employees.Add(empEntity);
                context.SaveChanges();
                return empEntity.Id;
            }
        }
    }
}