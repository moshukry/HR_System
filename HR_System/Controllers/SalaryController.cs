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


    
        // get days in month 
        var daysInMonth = DateTime.DaysInMonth(2011,11);


        // Get days of Fridays & Satardays in  a month 
        int Fridays = 0;
        int Saturdays = 0;
        int m_Month =11;
        int m_Year = 2011;

        DateTime dt = new DateTime(m_Year, m_Month, 1); // 1/11/2011
      
        while (dt.Month == m_Month)
        {
            if (dt.DayOfWeek == DayOfWeek.Friday)
            {
                Fridays++;
            }  

            if (dt.DayOfWeek == DayOfWeek.Saturday)
            {
                Saturdays++;
            }
            dt = dt.AddDays(1); // i++
        }


        // Get official yearly vacations from vacation table

        int yearlyVacs = db.Vacations.Where(n => n.VacationDate.Month == 11  && n.VacationDate.Year == 2011).Count();

        //**********
        // get Work Hours  [working hours from Employee - Work from Attendence ]
        float plusPerHour = db.Settings.Select(r => r.PlusPerhour).FirstOrDefault() ;
        float minusPerHour = db.Settings.Select(r => r.MinusPerhour).FirstOrDefault();

        foreach (var emp in employees)
        {

            double  workingHoursEmployee =  emp.DepartureTime.TotalMinutes /60 - emp.AttTime.TotalMinutes /60 ;// * working days

            //  var workingHoursAttendence  = db.Entry(emp).Collection(n => n.AttDeps).Query().Where(n =>   n.Date.Year == 2011 && n.Date.Month == 11).Select(n =>new { n.Departure  , n.Attendance}).ToList().Sum(n => n.Departure.TotalHours - n.Attendance.TotalHours) ;


            // get total bonus hours
            // get Minus bonus hours

            var BonusHours = db.Entry(emp).Collection(n => n.AttDeps)
                .Query()
                .Where(n => n.Date.Year == 2011 && n.Date.Month == 11 && n.workedHours / 60 > workingHoursEmployee)
                .Select(n => n.workedHours).ToList()
                .Sum( n => n - workingHoursEmployee *60);


            var MinusHours = db.Entry(emp).Collection(n => n.AttDeps)
             .Query()
             .Where(n => n.Date.Year == 2011 && n.Date.Month == 11 && n.workedHours /60 < workingHoursEmployee)
             .Select(n => n.workedHours).ToList()
             .Sum( n =>  workingHoursEmployee * 60 - n );


            double HourPrice = emp.FixedSalary / (workingHoursEmployee * 30);

            salary.Add(new SalaryVM()
            {
                fixedSalary = emp.FixedSalary,
                employeeName = emp.EmpName,
                departmentName = emp.Dept.DeptName ,
                attendenceDays = db.Entry(emp).Collection(n => n.AttDeps).Query()
                .Where(n => n.Date.Month == 11 && n.Date.Year == 2011).Count(),
                abscenseDays = daysInMonth - Fridays - Saturdays - yearlyVacs - db.Entry(emp).Collection(n => n.AttDeps).Query().Where(n => n.Date.Month == 11 && n.Date.Year == 2011).Count() ,
                MinusHours = Math.Floor(MinusHours /60),
                BonusHours = Math.Floor(BonusHours /60 ),
                TotalBonus = Math.Floor((BonusHours / 60) * plusPerHour * HourPrice ) ,
                TotalMinus = Math.Floor((MinusHours / 60) * minusPerHour * HourPrice),
                NetSalary = Math.Floor( emp.FixedSalary + ( (BonusHours / 60) * plusPerHour * HourPrice) - (MinusHours / 60) * minusPerHour * HourPrice )
            });
        }


        return View(salary);
    }
}
