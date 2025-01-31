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
    public IActionResult LoadAllEmployees(string gender="All") {
      switch(gender.ToLower()) {
        case "all":
          return Ok(appDb.Employees.ToList());
        case "male":
          return Ok(appDb.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
        case "female":
          return Ok(appDb.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
        default:
          return BadRequest($"Value for gender must be All, Male, or Female. {gender} is invalid");
      }
    }
    [HttpGet]
    [Route("api/[controller]/{id}", Name = "getLinkWithId")]
    public IActionResult EmployeeById(int id) {
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
    [HttpDelete]
    [Route("api/[controller]/{id}")]
    public IActionResult Delete(int id) {
      try {
        Employee employee = appDb.Employees.Find(id);
        if(employee == null) {
          return NotFound($"Employee with id = {id} not found to delete");
        } else {
          appDb.Remove(employee);
          appDb.SaveChanges();
          return Ok(employee);
        }
      }catch(Exception ex) {
        return BadRequest(ex);
      }
    }
    [HttpPut]
    [Route("api/[controller]/{id}")]
    public IActionResult Put([FromRoute]int id, [FromBody] Employee employee) {
      try {
        Employee entity = appDb.Employees.Find(id);
        if(entity != null) {
          entity.FirstName = employee.FirstName;
          entity.LastName = employee.LastName;
          entity.Gender = employee.Gender;
          entity.Salary = employee.Salary;
          appDb.SaveChanges();
          return Ok(entity);
        } else {
          return NotFound($"Employee with id = {id} not found to update");
        }
      }catch(Exception ex) {
        return BadRequest(ex);
      }
    }
  }
}
