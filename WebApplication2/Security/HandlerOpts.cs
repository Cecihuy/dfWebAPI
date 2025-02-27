using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebApplication2.Security {
  public class HandlerOpts : AuthenticationHandler<SchemeOpts> {
    public HandlerOpts(
      IOptionsMonitor<SchemeOpts> options, 
      ILoggerFactory logger, 
      UrlEncoder encoder
    ):base(options,logger,encoder) {      
    }
    protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
      Claim[] claims = [
        new Claim(ClaimTypes.Name, "Seblak"), 
        new Claim(ClaimTypes.Version, "Pedas")
      ];
      ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Jajanan");
      ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
      AuthenticationTicket authenticationTicket = new AuthenticationTicket(claimsPrincipal, this.Scheme.Name);
      return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
    }
  }
}
