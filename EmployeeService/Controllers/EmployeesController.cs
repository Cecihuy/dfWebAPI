using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using EmployeeDataAccess;

namespace EmployeeService.Controllers {
  [EnableCorsAttribute("*", "*", "*")]
  [RequireHttps] //can also put in each action method
  public class EmployeesController : ApiController {
    public HttpResponseMessage Get(string department="All") {
      using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
        //matching for my existing database
        switch(department.ToLower()) {
          case "all":
            return Request.CreateResponse(HttpStatusCode.OK,
              entities.Employees.ToList());
          case "1":
            return Request.CreateResponse(HttpStatusCode.OK,
              entities.Employees.Where(e => e.Department == 1).ToList());
          case "2":
            return Request.CreateResponse(HttpStatusCode.OK,
              entities.Employees.Where(e => e.Department == 2).ToList());
          case "3 ":
            return Request.CreateResponse(HttpStatusCode.OK,
              entities.Employees.Where(e => e.Department == 3).ToList());
          default:
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
              "Value for department must be all,1,2,3. Your choice " + department + " is invalid");
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
