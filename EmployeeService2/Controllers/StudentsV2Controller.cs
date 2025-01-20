using EmployeeService2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService2.Controllers
{
  public class StudentsV2Controller : ApiController {
    List<StudentV2> students = new List<StudentV2>() {
      new StudentV2(){Id=1, FirstName="Tom2", LastName="T2"},
      new StudentV2(){Id=2, FirstName="Sam2", LastName="S2"},
      new StudentV2(){Id=3, FirstName="John2", LastName="J2"}
    };
    [Route("api/v2/studentsversion")]
    public IEnumerable<StudentV2> Get() {
      return students;
    }
    [Route("api/v2/studentsversion/{id}")]
    public StudentV2 Get(int id) {
      return students.FirstOrDefault(s => s.Id == id);
    }
  }
}
