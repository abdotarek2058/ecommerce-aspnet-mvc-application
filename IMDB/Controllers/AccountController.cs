using IMDB.Data;
using IMDB.Data.Static;
using IMDB.Data.ViewModel;
using IMDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        [Authorize(Roles = UserRoles.Admin)]
        public async Task <IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user =await _userManager.GetUserAsync(User);
            var model = new ProfileVM()
            {
                FullName = user.FullName
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileVM model) 
        { 
            var user = await _userManager.GetUserAsync(User);
            if(!ModelState.IsValid)
                return View(model);
            // تحديث الاسم
            user.FullName = model.FullName;
            await _userManager.UpdateAsync(user);

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                var result = await _userManager.ChangePasswordAsync(
                    user,
                    model.CurrentPassword,
                    model.NewPassword
                    );
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }
            ViewBag.Success = "Profile updated successfully";
            return View(model);
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }
            return BadRequest();
        }
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> ChangeRole(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            // منع تعديل الأدمن الرئيسي
            if (user.Email == "admin@imdb.com")
                return BadRequest("Cannot modify main admin");

            // إزالة كل الأدوار الحالية
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            // إضافة الدور المختار
            var result = await _userManager.AddToRoleAsync(user, role);

            if (result.Succeeded)
                return RedirectToAction("Users");

            return BadRequest();
        }

        //public async Task<IActionResult> PromoteToAdmin(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var result = await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("User");
        //    }
        //    return BadRequest();
        //}
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName)?? await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password,model.RememberMe,false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Movies");
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Register model) { 
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName = model.FullName.Replace(" ",""),
                Email = model.Email,
                FullName = model.FullName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Movies");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
