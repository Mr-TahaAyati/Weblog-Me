using CoreLayer.DTOs.Users;
using CoreLayer.Services.Users;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Weblog.Pages.Shared.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }



        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "رمز عبور خود را وارد کنید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید بیشتر از 5 کارکتر باشه")]
        public string Password { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            var result = _userService.UserLogin(new UserLoginDto()
            {
                Password = Password,
                UserName = UserName
            });
            if (result.Status == OperationResultStatus.NotFound)
            {
                ModelState.AddModelError("UserName", "حساب مورد نظر پیدا نشد");
                return Page();
            }
            return RedirectToPage("../Index");
        }
    }
}
