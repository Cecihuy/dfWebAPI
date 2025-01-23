using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.DataAccess;

namespace WebApplication1 {
  public class Program {
    public static void Main(string[] args) {
      var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(options => 
          options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
        );
      var app = builder.Build();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
  }
}
