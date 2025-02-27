using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers {
  [ApiController]
  public class TokenController : ControllerBase {
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    public TokenController(
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager
    ) {
      this.userManager=userManager;
      this.signInManager=signInManager;
    }
    [HttpPost]
    [Route("api/[controller]")]    
    public async Task<IActionResult> Token([FromBody] Token model) {
      IdentityUser? user = await userManager.FindByNameAsync(model.UserName);
      if(user == null || !await userManager.CheckPasswordAsync(user, model.Password)) {
        return Unauthorized();
      }
      Claim[] claims = [
        new Claim(ClaimTypes.Name, "pertamax@m4il.IT"),
        new Claim(ClaimTypes.Email, "pertamax@m4il.IT")
      ];
      ClaimsIdentity ci = new ClaimsIdentity(claims, "Employee");
      ClaimsPrincipal cp = new ClaimsPrincipal(ci);
      Task result = Task.Run(async () =>
      await AuthenticationHttpContextExtensions.SignInAsync(this.HttpContext, cp));
      return Ok(result);
    }



    //[HttpPost]
    //[Route("api/[controller]")]
    //public async Task<IActionResult> Token([FromForm] string username, [FromForm] string password) {
    //  IdentityUser? user = await userManager.FindByNameAsync(username);
    //  if(user == null || !await userManager.CheckPasswordAsync(user, password)) {
    //    return Unauthorized();
    //  }
    //  const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    //  StringBuilder random = new StringBuilder();
    //  int digits = 10;
    //  while(digits-- > 0) {
    //    var num = BitConverter.ToUInt32(RandomNumberGenerator.GetBytes(4));
    //    random.Append(validChars[(int)(num % (uint)validChars.Length)]);
    //  }
    //  AuthenticationToken token = new AuthenticationToken {
    //    Name = "Bearer",
    //    Value = random.ToString()
    //  };
    //  return Ok(new {
    //    access_token = token.Value,
    //    token_type = token.Name,
    //    expires_in = 3600
    //  });
    //}
  }
}
