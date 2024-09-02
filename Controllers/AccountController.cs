using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Party_Management.Enums;
using Party_Management.Models;
using Party_Management.DTOs;

namespace Party_Management.Controllers
{
    [Route("[controller]/[action]")]
    //[AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        // Inject DI in constructor
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password); // Business Logic Method to Crete a User Create by UserManager
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return View(registerDTO);
            }

            if (registerDTO.UserType == UserRoleOptions.Admin)
            {
                // Create Admin Role
                if (await _roleManager.FindByNameAsync(UserRoleOptions.Admin.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole() { Name = UserRoleOptions.Admin.ToString() };
                    await _roleManager.CreateAsync(applicationRole);
                }
                await _userManager.AddToRoleAsync(user, UserRoleOptions.Admin.ToString());
            }
            else
            {
                if (await _roleManager.FindByNameAsync(UserRoleOptions.User.ToString()) is null)
                {
                    ApplicationRole applicationRole = new ApplicationRole() { Name = UserRoleOptions.User.ToString() };
                    await _roleManager.CreateAsync(applicationRole);
                }
                await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction(nameof(PartyController.Index), "Party");
        }

        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }

            var user = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);

            if (!user.Succeeded)
            {
                ModelState.AddModelError("Login", "Invalid credentials");
                return View(loginDTO);
            }

            // check user in admin or not

            //ApplicationUser? userForRole = await _userManager.FindByEmailAsync(loginDTO.Email);
            //if (user != null)
            //{
            //    if (await _userManager.IsInRoleAsync(userForRole, UserRoleOptions.Admin.ToString()))
            //    {
            //        return RedirectToAction("Index", "Home", new { area = "Admin" });
            //    }
            //}

            return RedirectToAction(nameof(PartyController.Index), "Party");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(PartyController.Index), "Party");
        }



        [AllowAnonymous]
        public async Task<IActionResult> IsEmailAlreadyRegister(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true); // User not Exits
            }
            return Json(false);
        }
    }
}
