using EmployeeService2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService2.Controllers {
  public class StudentsController : ApiController {
    static List<Student> students = new List<Student>() {
      new Student{Id = 1, Name = "Tome"},
      new Student{Id = 2, Name = "Sam"},
      new Student{Id = 3, Name = "John"}
    };
    public IEnumerable<Student> Get() {
      return students;
    }
    public Student Get(int id) {
      return students.FirstOrDefault(s => s.Id == id);
    }
    [Route("api/students/{id}/courses")]
    public IEnumerable<string> GetStudentCourses(int id) {
      if(id == 1) {
        return new List<string>() { "C#", "ASP.NET", "SQL SERVER" };
      } else if (id == 2){
        return new List<string>() { "ASP.NET WEB API", "C#", "SQL SERVER" };
      } else {
        return new List<string>() { "Bootstrap", "jQuery", "AngularJs" };
      }
    }
  }
}
