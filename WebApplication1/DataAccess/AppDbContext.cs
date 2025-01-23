using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.DataAccess {
  public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions options) : base(options) {
      
    }
    public DbSet<Employee> Employees { get; set; }
  }
}
