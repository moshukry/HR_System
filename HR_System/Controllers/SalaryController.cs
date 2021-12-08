using Microsoft.AspNetCore.Mvc;
using HR_System.Models;
using HR_System.ViewModels;

namespace HR_System.Controllers;
public class SalaryController : Controller
{
        HrSysContext db;

        public SalaryController(HrSysContext db)
        {
               this.db = db;
        }

    public IActionResult Index()
    { 

        List<SalaryVM> salary = new List<SalaryVM>();

        List<Employee> employees = db.Employees.ToList();

        List<AttDep> attendence = db.Att_dep.ToList();


        // Attendence Days using Explicit Loading 
        //var x = db.Employees.Single( n => n.EmpId == 1);
        //var y = db.Entry(x).Collection(n => n.AttDeps).Query().Count();
        
        foreach (var emp in employees)
        {
            salary.Add(new SalaryVM()
            {
                fixedSalary = emp.FixedSalary, 
                employeeName = emp.EmpName,
                departmentName = emp.Dept.DeptName ,
                attendenceDays = db.Entry(db.Employees.Single(n => n.EmpId == emp.EmpId)).Collection(n => n.AttDeps).Query().Where(n=> n.Date.Month==11 && n.Date.Year == 2011).Count() ,
               // abscenseDays =  db.Entry(db.Employees.Single(n =>n.EmpId == emp.EmpId)).Collection(n => n.AttDeps).Query().Where(n=> n.Date.Month == 11 && n.Date.Year == 2011).Count() 
            });


        }


        return View(salary);
    }
}
