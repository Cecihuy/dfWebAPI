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
    public HttpResponseMessage Get(int id) {
      using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
        var entity = entities.Employees.FirstOrDefault(e => e.Id == id);
        if(entity != null) {
          return Request.CreateResponse(HttpStatusCode.OK, entity);
        } else {
          return Request.CreateErrorResponse(
            HttpStatusCode.NotFound, "Employee with id = " + id.ToString() + " not found"
          );
        }
      }
    }
    public HttpResponseMessage Post([FromBody]Employee employee) {
      try {
        using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
          entities.Employees.Add(employee);
          entities.SaveChanges();
          var message = Request.CreateResponse(HttpStatusCode.Created, employee);
          message.Headers.Location = new Uri(Request.RequestUri + employee.Id.ToString());
          return message;
        }
      } catch(Exception ex) {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
      }
    }
  }
}
