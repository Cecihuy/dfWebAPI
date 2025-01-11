using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace EmployeeService {
  public class EmployeeSecurity {
    public static bool Login(string username, string password) {
      using(EmployeeDbEntities entities = new EmployeeDbEntities()) {
        return entities.Users.Any(u => u.Username.ToString() == username && u.Password.Equals(password));
      }
    }
  }
}