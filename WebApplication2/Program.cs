using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
      builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();
      /* ========== pipelines ========== */
      var app = builder.Build();
      //app.UseStaticFiles();
      app.UseFileServer();
      //app.UseAuthentication();
      app.MapControllers();
      app.Run();
    }
  }
}
