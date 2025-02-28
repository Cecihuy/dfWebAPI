using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.DataDb;
using WebApplication2.Services;

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
        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
          ValidateLifetime = true,
          RequireExpirationTime = true,
          ValidateIssuer = true,
          ValidIssuer = builder.Configuration["JWT:Issuer"],
          ValidateAudience = true,
          ValidAudience = builder.Configuration["JWT:Audience"],
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SignInKey"])
          )
        };
      });
      builder.Services.AddScoped<JwtTokenService>();
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
