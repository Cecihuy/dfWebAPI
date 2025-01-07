using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EmployeeDataAccess;

namespace EmployeeService.Controllers {
  public class EmployeesController : ApiController {
    [HttpGet]
    public IEnumerable<Employee> LoadAllEmployees() {
      using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
        return entities.Employees.ToList();
      }
    }
    [HttpGet]
    public HttpResponseMessage LoadEmployeeById(int id) {
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
    public HttpResponseMessage Delete(int id) {
      try {
        using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
          var entity = entities.Employees.FirstOrDefault(e => e.Id ==id);
          if(entity == null) {
            return Request.CreateErrorResponse(
              HttpStatusCode.NotFound, "Employee with id = " + id.ToString() + " not found to delete"
            );
          } else {
            entities.Employees.Remove(entity);
            entities.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
          }
        }
      } catch(Exception ex) {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
      }
    }
    public HttpResponseMessage Put(int id, [FromBody]Employee employee) {
      try {
        using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
          var entity = entities.Employees.FirstOrDefault(e => e.Id == id);
          if(entity == null) {
            return Request.CreateErrorResponse(
              HttpStatusCode.NotFound, "Employee with id = " + id.ToString() + " not found to update"
            );
          } else {
            entity.Name = employee.Name;
            entity.Email = employee.Email;
            entity.Department = employee.Department;
            entity.PhotoPath = employee.PhotoPath;
            entities.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, entity);
          }
        }
      } catch(Exception ex) {
        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
      }
    }
  }
}
