using EmployeeService2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService2.Controllers
{
  public class StudentsV1Controller : ApiController {
    List<StudentV1> students = new List<StudentV1>() {
      new StudentV1(){Id=1,Name="Tom1"},
      new StudentV1(){Id=2,Name="Sam1"},
      new StudentV1(){Id=3,Name="John1"}
    };
    [Route("api/v1/studentsversion")]
    public IEnumerable<StudentV1> Get() {
      return students;
    }
    [Route("api/v1/studentsversion/{id}")]
    public StudentV1 Get(int id) {
      return students.FirstOrDefault(s => s.Id == id);
    }
  }
}
