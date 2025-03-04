﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService2.Controllers{
  [Authorize]
  public class EmployeesController : ApiController {
    public IEnumerable<Employee> Get() {
      using(EmployeeDBEntities entities = new EmployeeDBEntities()) {
        return entities.Employees.ToList();
      }
    }
  }
}
