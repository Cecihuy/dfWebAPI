using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApplication2.DataDb;

namespace WebApplication2 {
  public class Program {
    public static void Main(string[] args) {
      /* ========== services ========== */
      var builder = WebApplication.CreateBuilder(args);      
      builder.Services.AddControllers();
      builder.Services.AddSqlServer<AppDbContext>(builder.Configuration.GetConnectionString("sqlServerConn"));
      builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();
      builder.Services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
        options.DefaultForbidScheme =
        options.DefaultScheme =
        options.DefaultSignInScheme =
        options.DefaultSignOutScheme = BearerTokenDefaults.AuthenticationScheme;
      }).AddBearerToken(options => {
        options.BearerTokenExpiration = TimeSpan.FromSeconds(30);
      });
      /* ========== pipelines ========== */
      var app = builder.Build();
      app.UseFileServer();
      app.UseAuthentication();
      app.UseAuthorization();
      app.MapControllers();
      app.Run();
    }
  }
}
