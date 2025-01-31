using Microsoft.AspNetCore.Mvc;
using System;
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
    [HttpGet]
    [Route("api/[controller]")]
    public IEnumerable<Employee> Get() {
      return appDb.Employees.ToList();
    }
    [HttpGet]
    [Route("api/[controller]/{id}", Name = "getLinkWithId")]
    public IActionResult GetById(int id) {
      Employee employee = appDb.Employees.FirstOrDefault(e => e.Id == id);
      if(employee != null) {
        return Ok(employee);
      } else {
        return NotFound($"Employee with id = {id} not found");
      }
    } 
    [HttpPost]
    [Route("api/[controller]")]
    public IActionResult Post([FromBody] Employee employee) {
      try {
        appDb.Employees.Add(employee);
        appDb.SaveChanges();
        return Created(
          new Uri(
            Url.Link("getLinkWithId", new { id = employee.Id }).ToLower()
          ), employee
        );
      } catch(Exception ex) {
        return BadRequest(ex);
      }
    }
  }
}
