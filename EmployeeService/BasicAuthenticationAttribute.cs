using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeService {
  public class BasicAuthenticationAttribute : AuthorizationFilterAttribute{
    // hasil encode dari website base64encode.org
    // username:password = base64encode request header value
    //        1:satu     = Basic MTpzYXR1
    //        2:dua      = Basic MjpkdWE=
    public override void OnAuthorization(HttpActionContext actionContext) {
      if(actionContext.Request.Headers.Authorization == null) {
        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
      } else {
        string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
        string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
        string[] userNamePasswordArray = decodedAuthenticationToken.Split(':');
        string username = userNamePasswordArray[0];
        string password = userNamePasswordArray[1];
        if(EmployeeSecurity.Login(username, password)) {
          Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
        } else {
          actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
      }
    }
  }
}