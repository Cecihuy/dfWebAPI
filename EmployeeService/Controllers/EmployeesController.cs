using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers {
  public class EmployeesController : ApiController {
    public IEnumerable<Employee> Get() {
      using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
        return entities.Employees.ToList();
      }
    }
    public Employee Get(int id) {
      using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
        return entities.Employees.FirstOrDefault(e => e.Id == id);
      }
    }
  }
}
