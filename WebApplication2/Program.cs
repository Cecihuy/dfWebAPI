using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication2.Data;

namespace WebApplication2 {
  public class Program {
    public static void Main(string[] args) {
      /* ========== services ========== */
      var builder = WebApplication.CreateBuilder(args);
      builder.Services.AddControllers();
      builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("sqlServerConn"));      
      /* ========== pipelines ========== */
      var app = builder.Build();
      app.MapControllers();
      app.Run();
    }
  }
}
