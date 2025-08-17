using ComputerShop.Areas.Admin.Repository; // Đảm bảo import đúng
using ComputerShop.Models;
using ComputerShop.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _signInManager;
        private readonly IAppEmailSender _emailSender; // Thay đổi thành IAppEmailSender

        public AccountController(IAppEmailSender emailSender, SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender; // Tiêm IAppEmailSender
        }
        [HttpGet]
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl});
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(loginVM.Username, loginVM.Password, false, lockoutOnFailure: true);

				if (result.Succeeded)
				{
					return Redirect(loginVM.ReturnUrl ?? "/");
				}
				else
				{
					ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
				}
			}
			return View(loginVM);
		}


		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid)
			{
				var newUser = new AppUserModel { UserName = user.Username, Email = user.Email };
				var result = await _userManager.CreateAsync(newUser, user.Password);
				if (result.Succeeded)
				{
					TempData["success"] = "Tạo tài khoản thành công!";
					return Redirect("/account/login");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View();
		}

		public async Task<IActionResult> Logout(string returnUrl = "/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
		
	}
}
