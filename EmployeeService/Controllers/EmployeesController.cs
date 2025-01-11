using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using EmployeeDataAccess;

namespace EmployeeService.Controllers {
  [EnableCorsAttribute("*", "*", "*")]
  public class EmployeesController : ApiController {
    [BasicAuthenticationAttribute]
    public HttpResponseMessage Get(string department="All") {
      string username = Thread.CurrentPrincipal.Identity.Name;
      using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
        //matching for my existing database
        switch(username.ToLower()) {
          case "1":
            return Request.CreateResponse(HttpStatusCode.OK,
              entities.Employees.Where(e => e.Department == 1).ToList());
          case "2":
            return Request.CreateResponse(HttpStatusCode.OK,
              entities.Employees.Where(e => e.Department == 2).ToList());
          default:
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
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
    //[DisableCors]
    public HttpResponseMessage Put([FromBody]int id, [FromUri]Employee employee) {
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
