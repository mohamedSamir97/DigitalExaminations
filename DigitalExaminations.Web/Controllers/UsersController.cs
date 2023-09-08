using DigitalExaminations.BLL.Services;
using DigitalExaminations.ViewModels;
using DigitalExaminations.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DigitalExaminations.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            LoginViewModel sessionObj = HttpContext.Session.Get<LoginViewModel>("loginvm");
            if (sessionObj == null)
            {
                return RedirectToAction("Login","Account");
            }

                return View(_accountService.GetAllTeachers(pageNumber, pageSize));
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isExists = _accountService.UserExistsAsync(userViewModel.UserName, userViewModel.Role);
                if (isExists)
                {
                    ModelState.AddModelError(string.Empty, "User Name already exists.");
                    return View(userViewModel);
                }
                await _accountService.AddUser(userViewModel);

                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }
    }
}
