using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml;
using WebApplication1.DataAccess;

namespace WebApplication1 {
  public class Program {
    public static void Main(string[] args) {
      /* ========== variables ========== */
      JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
      };
      XmlWriterSettings xmlWriterSettings = new XmlWriterSettings() {
        Indent = true
      };
      /* ========== services ========== */
      var builder = WebApplication.CreateBuilder(args);      
      builder.Services.AddControllers(options => {
        options.RespectBrowserAcceptHeader = true;
        options.OutputFormatters.Clear();
        options.OutputFormatters.Add(new XmlSerializerOutputFormatter(xmlWriterSettings));
        options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(jsonSerializerOptions));
      });
      builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"))
      );
      builder.Services.AddCors(options => {
        options.AddPolicy("freedom", pol => {
          pol.AllowAnyHeader()
             .AllowAnyMethod()
             .AllowAnyOrigin()
             .Build();
        });
        options.AddPolicy("custom", pol => {
          pol.AllowAnyHeader()
             .WithOrigins("https://localhost:44368")
             .WithMethods("GET")
             .Build();
        });
      });
      /* ========== pipelines ========== */
      var app = builder.Build();
      app.UseFileServer();      
      app.UseAuthorization();      
      app.MapControllers();
      app.UseCors();
      app.Run();
    }
  }
}