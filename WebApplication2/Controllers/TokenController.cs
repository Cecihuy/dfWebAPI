using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers {
  [ApiController]
  public class TokenController : ControllerBase {
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly JwtTokenService jwtTokenService;
    public TokenController(
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager,
      JwtTokenService jwtTokenService
    ) {
      this.userManager=userManager;
      this.signInManager=signInManager;
      this.jwtTokenService=jwtTokenService;
    }
    [HttpPost]
    [Route("api/[controller]")]    
    public async Task<IActionResult> Token([FromBody] Token model) {
      IdentityUser? user = await userManager.FindByNameAsync(model.UserName);
      if(user == null || !await userManager.CheckPasswordAsync(user, model.Password)) {
        return Unauthorized();
      }
      return Ok(new JwtToken {
        UserName = model.UserName,
        Email = model.UserName,
        Token = jwtTokenService.CreateToken(user)
      });
    }
  }
}
