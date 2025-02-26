using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.DataDb {
  public class AppDbContext : IdentityDbContext {
    public AppDbContext(DbContextOptions options):base(options) {
      
    }
    public DbSet<Employee> Employees { get; set; }
  }
}
