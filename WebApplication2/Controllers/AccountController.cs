using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers {
  [ApiController]
  public class AccountController : ControllerBase {
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager
    ) {
      this.userManager=userManager;
      this.signInManager=signInManager;
    }
    [HttpPost]
    [Route("api/[controller]/register")]
    public async Task<IActionResult> Register(Register model) {
      if(ModelState.IsValid) {
        IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
        IdentityResult identityResult = await userManager.CreateAsync(user, model.Password);
        if(identityResult.Succeeded) {
          await signInManager.SignInAsync(user, false);
        }
        foreach(var error in identityResult.Errors) {
          ModelState.AddModelError("", error.Description);
        }
      }
      return Ok();
    }
  }
}
