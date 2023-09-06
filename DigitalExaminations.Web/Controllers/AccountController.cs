using DigitalExaminations.BLL.Services;
using DigitalExaminations.ViewModels;
using DigitalExaminations.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DigitalExaminations.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            LoginViewModel sessionObj = HttpContext.Session.Get<LoginViewModel>("loginvm");
            if (sessionObj == null)
                return View();
            else
            {
                return RedirectUser(sessionObj);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                LoginViewModel loginVm = _accountService.Login(loginViewModel);

                if (loginVm != null)
                {
                    HttpContext.Session.Set<LoginViewModel>("loginvm", loginVm);
                    return RedirectUser(loginVm);
                }
            }
            // If ModelState is not valid, add a custom error message
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(loginViewModel);
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Set<LoginViewModel>("loginvm", null);
            }
            catch (Exception ex)
            {

                
            }
            

            return RedirectToAction("Login");
        }

       

        public IActionResult RedirectUser(LoginViewModel loginViewModel)
        {
            if (loginViewModel.Role == (int)EnumRoles.Admin)
            {

                return RedirectToAction("Index", "Users");
            }
            else if (loginViewModel.Role == (int)EnumRoles.Teacher)
            {

                return RedirectToAction("Index", "Exams");
            }
            return RedirectToAction("Profile", "Students");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
