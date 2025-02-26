using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using WebApplication2.DataDb;

namespace WebApplication2.Controllers {
  [ApiController]
  public class EmployeesController : ControllerBase {
    private readonly AppDbContext appDb;
    public EmployeesController(AppDbContext appDb) {
      this.appDb=appDb;
    }
    [Route("api/employees")]
    [Authorize]
    public IEnumerable Get() {
      return appDb.Employees.ToList();
    }
  }
}
