using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularJSPOC.EntityFramework;
using AngularJSPOC.BusinessObject;

namespace AngularJSPOC.Controllers
{
    public class EmployeeController : Controller
    {
        private AngularJSPOCEmployeeEntities db = new AngularJSPOCEmployeeEntities();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();

        }

        public ActionResult GetEmployees()
        {
            List<EmployeeModel> empEntity = new List<EmployeeModel>();
            var employees = db.Employees.Include(s => s.State).ToList();
            int counter = 0;
            foreach (var x in employees)
            {
                EmployeeModel obj = new EmployeeModel();
                obj.EmployeeId = x.Id;
                obj.EmployeeName = x.Name;
                obj.EmployeeAge = x.Age;
                obj.EmployeeEmail = x.Email;
                obj.StateName = x.State.StateName;
                empEntity.Add(obj);
            }
            List<EmployeeModel> employeeList = empEntity;
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCountriesAndStates()
        {
            var list = db.Countries.Include("State").Select(x => new { x.CountryName, x.CountryId, States = x.States.Select(y => new { y.StateId, y.StateName }) }).ToList();
            var obj = Json(list, JsonRequestBehavior.AllowGet);
            return obj;
        }

        public JsonResult GetCountries()
        {
            var ret = db.Countries.Select(x => new { x.CountryId, x.CountryName }).ToList();
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStates()
        {
            var ret = db.States.Select(x => new { x.StateId, x.StateName }).ToList();
            return Json(ret,JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Id > 0)
                {
                    var emp = db.Employees.Find(employee.Id);
                    emp.Name = employee.Name;
                    emp.Age = employee.Age;
                    emp.Email = employee.Email;
                    emp.StateId = employee.StateId;
                }
                else
                {
                    db.Employees.Add(employee);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", employee.StateId);
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult EditEmployee(int id)
        {
            ViewBag.EmpId = id;
            return View("Create");
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.EmpId = id;
            var emp = db.Employees.Include("State").Where(e => e.Id == id).Select(x => new EmployeeModel
            {
                EmployeeId = x.Id,
                EmployeeName = x.Name,
                EmployeeAge = x.Age,
                EmployeeEmail = x.Email,
                StateId = x.State.StateId,
                StateName=x.State.StateName,
                CountryId = x.State.Country.CountryId,
                CountryName = x.State.Country.CountryName,
            }).SingleOrDefault();
            //var list = db.Countries.Include("State").Select(x => new { x.CountryName, x.CountryId, States = x.States.Select(y => new { y.StateId, y.StateName }) }).ToList();
            //Employee employee = db.Employees.Find(id);
            if (emp == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            //ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", employee.StateId);
            //EmployeeModel obj = new EmployeeModel();
            //obj.EmployeeId = employee.Id;
            //obj.EmployeeName = employee.Name;
            //obj.EmployeeAge = employee.Age;
            //obj.EmployeeEmail = employee.Email;
            //obj.StateId = employee.State.StateId;
            //obj.CountryId =employee.State.Country.CountryId;
            //obj.CountryName = employee.State.Country.CountryName;
            var obj = Json(emp, JsonRequestBehavior.AllowGet);
            return obj;
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", employee.StateId);
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}