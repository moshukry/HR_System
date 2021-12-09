using HR_System.Models;
namespace HR_System.ViewModels;
public class SalaryVM
{
    public string employeeName { get; set; }
    public string departmentName { get; set; }
    public int fixedSalary { get; set; }
   
    public int attendenceDays {  get; set; }

    public int abscenseDays {  get; set; }

    public double BonusHours { get; set; }

    public double MinusHours { get; set; }

    public double TotalBonus { get; set; }
    public double TotalMinus { get; set; }

    public double NetSalary { get; set; }







}
