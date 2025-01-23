using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WebApplication1.DataAccess;

namespace WebApplication1 {
  public class Program {
    public static void Main(string[] args) {
      /* ========== services ========== */
      var builder = WebApplication.CreateBuilder(args);
      builder.Services.AddControllers();
      //  .AddNewtonsoftJson(options => {
      //    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
      //    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
      //  });
      builder.Services.Configure<JsonOptions>(options => {
        //options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;        
        options.JsonSerializerOptions.WriteIndented = true;
      });
      builder.Services.AddDbContext<AppDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
      );      
      /* ========== pipelines ========== */
      var app = builder.Build();
      app.UseAuthorization();
      app.MapControllers();
      app.Run();
    }
  }
}
