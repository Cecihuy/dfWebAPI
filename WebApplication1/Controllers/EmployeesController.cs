using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.DataAccess;
using WebApplication1.Model;

namespace WebApplication1.Controllers {
  [ApiController]
  public class EmployeesController : ControllerBase {
    private readonly AppDbContext appDb;
    public EmployeesController(AppDbContext appDb) {
      this.appDb = appDb;
    }
    [Route("api/[controller]")]
    public IEnumerable<Employee> Get() {
      return appDb.Employees.ToList();
    }
    [Route("api/[controller]/{id}")]
    public Employee GetById(int id) {
      return appDb.Employees.FirstOrDefault(e => e.Id == id);
    }
  }
}
