using EmployeeService2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService2.Controllers{
  public class IhttpController : ApiController
  {
    static List<Student> students = new List<Student>(){
      new Student{Id=1,Name="Tome"},
      new Student{Id=2,Name="Sam"},
      new Student{Id=3,Name="John"}
    };
    //public HttpResponseMessage Get() {
    //  return Request.CreateResponse(HttpStatusCode.OK, students);
    //}
    public IHttpActionResult Get() {
      return Ok(students);
    }
    public IHttpActionResult Get(int id) {
      var student = students.FirstOrDefault(s => s.Id == id);
      if(student == null) {
        return Content(HttpStatusCode.NotFound, "student with id " + id + " notfound");
      }
      return Ok(student);
    }
  }    
}
