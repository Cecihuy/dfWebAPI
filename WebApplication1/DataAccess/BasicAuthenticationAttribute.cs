using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace WebApplication1.DataAccess {
  public class UserPassAttribute : ServiceFilterAttribute {
    public UserPassAttribute() : base(typeof(BasicAuthenticationAttribute)) {
    }
  }
  public class BasicAuthenticationAttribute : IAuthorizationFilter{
    private readonly AppDbContext appDb;
    public BasicAuthenticationAttribute(AppDbContext appDb) {
      this.appDb = appDb;
    }
    public bool Login(string username, string password) {
      return appDb.Users.Any(user =>
        user.Username.Equals(username) &&
        user.Password == password
      );
    }
    public void OnAuthorization(AuthorizationFilterContext context) {
      try {
        string authenticationToken = context.HttpContext.Request.Headers.Authorization.ToString();
        string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
        string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
        string username = usernamePasswordArray[0];
        string password = usernamePasswordArray[1];
        if(Login(username, password))
          Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
      } catch (Exception ex) {

      }
    }
  }
}
