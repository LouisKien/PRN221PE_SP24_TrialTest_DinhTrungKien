using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221PE_SP24_TrialTest_DinhTrungKien.Repo.Interfaces;
using PRN221PE_SP24_TrialTest_DinhTrungKien_Repo.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace PRN221PE_SP24_TrialTest_DinhTrungKien.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public LoginInfo LoginInfo { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                StoreAccount? userInfo = _unitOfWork.StoreAccountRepository.Find(a => a.EmailAddress == LoginInfo.Email && a.AccountPassword == LoginInfo.Password).FirstOrDefault();
                if (userInfo != null)
                {
                    if(userInfo.Role == 3 || userInfo.Role == 4)
                    {
                        ModelState.AddModelError(string.Empty, "You do not have permission to do this function!");
                        return Page();
                    }
                    HttpContext.Session.SetString("Email", userInfo.EmailAddress);
                    HttpContext.Session.SetString("Name", userInfo.FullName);
                    HttpContext.Session.SetString("Role", userInfo.Role.ToString());
                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
            }

            return Page();
        }
    }

    public class LoginInfo
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
