using EmployeeListFromBody.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeListFromBody.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpPost("addlist")]
        public IActionResult AddEmployee([FromBody] List<Employee> employee)
        {
            int count = employee.Count;
            EmployeeList.Employees.AddRange(employee);
            return Ok($"Total Employee Received {count}");
        }
        [HttpPost("GetEmployee")]
        public IActionResult GetAllEmployee()
        {
            return Ok(EmployeeList.Employees);
        }
        [HttpPost("TotalSalary")]
        public IActionResult GetTotalSalary()
        {
            var res = EmployeeList.Employees.Sum(x => x.Salary);
            return Ok(res);
        }
    }
}
